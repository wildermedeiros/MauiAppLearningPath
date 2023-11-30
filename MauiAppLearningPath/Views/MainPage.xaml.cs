using Firebase.Database;
using Firebase.Database.Query;
using MauiAppLearningPath.Models;
using MauiAppLearningPath.Utils;
using System.Collections.ObjectModel;

namespace MauiAppLearningPath.Views
{
    public partial class MainPage : ContentPage
    {
        readonly FirebaseClient FirebaseClient = new ("https://mauifirebasesilicon-default-rtdb.firebaseio.com/");

        public MainPage()
        {
            InitializeComponent();
        }

        private void NoteDescriptionEntryOnCompleted(object sender, EventArgs e)
        {
            if(!isDescriptionNullOrEmpty())
            {
                FirebaseClient.Child("Notes").PostAsync(new Note
                {
                    Description = NoteDescriptionEntry.Text,
                });
                NoteDescriptionEntry.Text = "";
            }
        }

        private bool isDescriptionNullOrEmpty()
        {
            return string.IsNullOrEmpty(NoteDescriptionEntry.Text);
        }
    }
}
