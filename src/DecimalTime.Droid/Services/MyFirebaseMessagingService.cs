using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Util;
using DecimalTime.Core.Utils;
using Firebase.Messaging;
using Java.Util.Concurrent.Atomic;

namespace DecimalTime.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        readonly string TAG = nameof(MyFirebaseMessagingService);

        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Info(TAG, "From: " + message.From);
            Log.Info(TAG, "Notification Message Body: " + message.GetNotification().Body);

            try {
                SendNotification(message.GetNotification().Body);
            } catch(Exception e) {
                IoC.Analytics.LogException(e, false);
            }
        }

        void SendNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(this)
                .SetVisibility(NotificationVisibility.Public)
                .SetSmallIcon(Resource.Drawable.ic_stat_ic_notification)
                .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.Icon))
                .SetContentTitle(GetString(Resource.String.ApplicationName))
                .SetContentText(messageBody)
                .SetAutoCancel(true)
                .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(NotificationID.GetID(), notificationBuilder.Build());
        }

        public static class NotificationID
        {
            private readonly static AtomicInteger c = new AtomicInteger(0);

            public static int GetID()
            {
                return c.IncrementAndGet();
            }
        }
    }
}