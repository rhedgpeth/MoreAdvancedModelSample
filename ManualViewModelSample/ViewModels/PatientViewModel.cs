using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManualViewModelSample.ViewModels
{
	public class PatientViewModel : BaseViewModel
	{
		public Action<bool> OnSaveComplete { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }

		List<string> nicknames;
		public List<string> Nicknames
		{
			get
			{
				if (nicknames == null)
					nicknames = new List<string>();

				return nicknames;
			}
			set
			{
				nicknames = value;
				Raise(nameof(Nicknames));
			}
		}

		public PatientViewModel()
		{ }

		public async Task Save()
		{
			// Do stuff to save

			if (OnSaveComplete != null)
				OnSaveComplete(true);
		}
	}
}

