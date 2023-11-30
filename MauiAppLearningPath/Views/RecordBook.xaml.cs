using Firebase.Database;
using MauiAppLearningPath.Models;
using MauiAppLearningPath.Utils;
using System.Collections.ObjectModel;

namespace MauiAppLearningPath.Views
{
	public partial class RecordBook : ContentPage
	{
        readonly FirebaseClient FirebaseClient = new("https://mauifirebasesilicon-default-rtdb.firebaseio.com/");

        public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

        public RecordBook()
		{
			InitializeComponent();

            BindingContext = this;
            DisplayDataFromServer();
        }

        private void DisplayDataFromServer()
        {
            var collection = FirebaseClient
                .Child("Notes")
                .AsObservable<Note>()
                .Subscribe(item =>
                {
                    if (item.Object != null)
                    {
                        string NoteDescription = item.Object.Description;
                        if (int.TryParse(NoteDescription, out int StringToWorkOn))
                        {
                            NoteDescription = NumberConverter.NumberToWords(StringToWorkOn);
                        }

                        Notes.Add(new Note
                        {
                            Description = NoteDescription,
                        });
                    }
                });
        }
    }
}