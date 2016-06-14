using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ManualViewModelSample.ViewModels
{
	public partial class BaseViewModel : INotifyPropertyChanged
	{
		readonly List<KeyValuePair<string, List<Action>>> PropertyWatchers = new List<KeyValuePair<string, List<Action>>>();

		bool isBusy = false;

		public event PropertyChangedEventHandler PropertyChanged;

		public bool IsBusy
		{
			get
			{
				return isBusy;
			}

			set
			{
				RaiseAndUpdate(ref isBusy, value);
				Raise(nameof(IsNotBusy));
			}
		}

		public bool IsNotBusy => !IsBusy;

		protected void RaiseAndUpdate<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			field = value;
			Raise(propertyName);
		}

		protected void Raise(string propertyName)
		{
			if (!string.IsNullOrEmpty(propertyName) && PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}

			var watchers = PropertyWatchers.FirstOrDefault(pw => pw.Key == propertyName);
			if (watchers.Equals(default(KeyValuePair<string, List<Action>>)))
				return;

			foreach (Action watcher in watchers.Value)
			{
				watcher();
			}
		}

		public void WatchProperty(string propertyName, Action action)
		{
			if (PropertyWatchers.All(pw => pw.Key != propertyName))
			{
				PropertyWatchers.Add(new KeyValuePair<string, List<Action>>(propertyName, new List<Action>()));
			}

			PropertyWatchers.First(pw => pw.Key == propertyName).Value.Add(action);
		}

		public void ClearWatchers()
		{
			PropertyWatchers.Clear();
		}

		public virtual Task InitAsync()
		{
			return new Task(() => { });
		}

		/*
		protected async Task CacheValue<T>(T value, string propertyName)
		{
			var key = $"{GetType().Name}-{propertyName}";
			Console.WriteLine($"Setting object with id {key}");
			await BlobCache.LocalMachine.InsertObject(key, value);
		}

		protected async Task<T> GetFromCache<T>(string propertyName)
		{
			var key = $"{GetType().Name}-{propertyName}";
			Console.WriteLine($"Getting object with id {key}");
			return await BlobCache.LocalMachine.GetObject<T>(key);
		}*/
	}
}

