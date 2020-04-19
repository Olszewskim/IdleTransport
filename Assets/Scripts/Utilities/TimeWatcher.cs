using System;

namespace IdleTransport.Utilities {
    public class TimeWatcher : Singleton<TimeWatcher> {
        public static event Action OnAppGoPause;

        public static event Action<float> OnAppGoUnpause;

        private DateTime _startPauseTime;

        private void OnApplicationPause(bool pause) {
            if (pause) {
                _startPauseTime = DateTime.UtcNow;
                OnAppGoPause?.Invoke();
            } else {
                var pauseTimeInSeconds = _startPauseTime != default
                    ? (float) (DateTime.UtcNow - _startPauseTime).TotalSeconds
                    : 0;
                OnAppGoUnpause?.Invoke(pauseTimeInSeconds);
            }
        }
    }
}
