using System;
using Autofac;
using DecimalTime.Forms;
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
            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            // get permission for notification
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0)) {
                // iOS 10
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
                    Console.WriteLine(granted);
                });

                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;

                // For iOS 10 data message (sent via FCM)
                //Messaging.SharedInstance.RemoteMessageDelegate = this;
            } else {
                // iOS 9 <=
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            UIApplication.SharedApplication.RegisterForRemoteNotifications();

            // Firebase component initialize
            Firebase.Core.App.Configure();

            Firebase.InstanceID.InstanceId.Notifications.ObserveTokenRefresh((sender, e) => {
                var newToken = Firebase.InstanceID.InstanceId.SharedInstance.Token;
                // if you want to send notification per user, use this token
                Console.WriteLine(newToken);

                ConnectFCM();
            });

            IoCSetup();

            return base.FinishedLaunching(app, options);
        }

        private void IoCSetup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<FirebaseAnalyticsService>().As<AnalyticsService>().SingleInstance();
            builder.RegisterType<TextToSpeechService>().As<ITextToSpeech>().SingleInstance();
            IoC.Container = builder.Build();
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
            Messaging.SharedInstance.ApnsToken = deviceToken;
        }

        // iOS 9 <=, fire when recieve notification foreground
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            Messaging.SharedInstance.AppDidReceiveMessage(userInfo);

            // Generate custom event
            NSString[] keys = { new NSString("Event_type") };
            NSObject[] values = { new NSString("Recieve_Notification") };
            var parameters = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(keys, values, keys.Length);

            // Send custom event
            Firebase.Analytics.Analytics.LogEvent("CustomEvent", parameters);

            if (application.ApplicationState == UIApplicationState.Active) {
                Console.WriteLine(userInfo);
                var aps_d = userInfo["aps"] as NSDictionary;
                var alert_d = aps_d["alert"] as NSDictionary;
                var body = alert_d["body"] as NSString;
                var title = alert_d["title"] as NSString;
                ShowNotification(title, body);
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
