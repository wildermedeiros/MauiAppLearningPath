using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace MauiAppLearningPath
{
    public partial class MainPage : ContentPage
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://mauifirebasesilicon-default-rtdb.firebaseio.com/");

        public ObservableCollection<Note> Notes{ get; set; } = new ObservableCollection<Note>();


        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;

            var collection = firebaseClient
                .Child("Notes")
                .AsObservable<Note>()
                .Subscribe((item) =>
                {
                    if (item.Object != null)
                    {
                        Notes.Add(new Note
                        {
                            Description = item.Object.Description,
                        });
                    }
                });
        }

        private void OnSubmit(object sender, EventArgs e)
        {
            firebaseClient.Child("Notes").PostAsync(new Note
            {
                Description = NoteDescriptionEntry.Text,
            });
        }
    }

}
