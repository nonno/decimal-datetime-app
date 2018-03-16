using System;
using Autofac;
using DecimalTime.Forms;
using DecimalTime.Forms.Services;
using DecimalTime.Forms.Utils;
using DecimalTime.iOS.Services;
using Firebase.CloudMessaging;
using Foundation;
using UIKit;
using UserNotifications;

namespace DecimalTime.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate
    {
        private const string genericNotification = nameof(genericNotification);

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            IoCSetup();

            FirebaseSetup();

            Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void IoCSetup()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<FirebaseAnalyticsService>().As<AnalyticsService>().SingleInstance();
            containerBuilder.RegisterType<TextToSpeechService>().As<ITextToSpeechService>().SingleInstance();
            containerBuilder.RegisterType<LocalizationService>().As<ILocalizationService>().SingleInstance();
            containerBuilder.RegisterType<SettingsProvider>().As<SettingsProvider>().SingleInstance();
            IoC.Container = containerBuilder.Build();
        }

        private void FirebaseSetup()
        {
            Firebase.Core.App.Configure();

            // get permission for notification
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0)) {
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                    Console.WriteLine(granted);
                });

                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;

                // For iOS 10 data message (sent via FCM)
                //Messaging.SharedInstance.RemoteMessageDelegate = this;
            } else {
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            Firebase.InstanceID.InstanceId.Notifications.ObserveTokenRefresh((sender, e) => {
                var newToken = Firebase.InstanceID.InstanceId.SharedInstance.Token;
                // if you want to send notification per user, use this token
                Console.WriteLine(newToken);

                ConnectFCM();
            });
        }

        public override void DidEnterBackground(UIApplication uiApplication)
        {
            Messaging.SharedInstance.ShouldEstablishDirectChannel = false;
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            ConnectFCM();
            base.OnActivated(uiApplication);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Console.WriteLine("Token: " + deviceToken);
            Messaging.SharedInstance.ApnsToken = deviceToken;
        }

        [Export("messaging:didReceiveRegistrationToken:")]
        public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            Console.WriteLine($"Firebase registration token: {fcmToken}");
        }

        // iOS 9 <=, fire when recieve notification foreground
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            Messaging.SharedInstance.AppDidReceiveMessage(userInfo);

            // Generate custom event
            NSString[] keys = { new NSString("Event_type") };
            NSObject[] values = { new NSString("Receive_Notification") };
            var parameters = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(keys, values, keys.Length);

            // Send custom event
            Firebase.Analytics.Analytics.LogEvent("CustomEvent", parameters);

            if (application.ApplicationState == UIApplicationState.Active) {
                Console.WriteLine(userInfo);
                try
                {
                    var aps = userInfo["aps"] as NSDictionary;
                    var alert = aps["alert"] as NSDictionary;
                    if (alert != null) {
                        var title = alert["title"] as NSString ?? new NSString();
                        var body = alert["body"] as NSString ?? new NSString();

                        ShowNotification(title, body);
                    }
                } catch(Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // Receive data message on iOS 10 devices.
        public void ApplicationReceivedRemoteMessage(RemoteMessage remoteMessage)
        {
            Console.WriteLine(remoteMessage.AppData);
        }

        private void ConnectFCM()
        {
            Messaging.SharedInstance.ShouldEstablishDirectChannel = true;
        }

        private void ShowNotification(string title, string message)
        {
            //var alert = new UIAlertView(title ?? "Title", message ?? "Message", null, "Cancel", "OK");
            //alert.Show();

            InvokeOnMainThread(() => {
                if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0)) {
                    var content = new UNMutableNotificationContent {
                        Title = title,
                        Subtitle = message,
                        Sound = UNNotificationSound.Default,
                        Badge = 0
                    };
                    NSObject objTrue = new NSNumber(true);
                    NSString key = new NSString("shouldAlwaysAlertWhileAppIsForeground");
                    content.SetValueForKey(objTrue, key);
                    key = new NSString("shouldAddToNotificationsList");
                    content.SetValueForKey(objTrue, key);

                    var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(2, false);
                    var request = UNNotificationRequest.FromIdentifier(genericNotification, content, trigger);

                    UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) => {
                        if (err != null) {
                            Console.WriteLine("Error: " + err.LocalizedDescription);
                        }
                    });
                }
            });
        }
    }
}
