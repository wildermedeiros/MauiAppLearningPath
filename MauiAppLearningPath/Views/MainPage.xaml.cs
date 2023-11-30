using Firebase.Database;
using Firebase.Database.Query;
using MauiAppLearningPath.Models;

namespace MauiAppLearningPath.Views
{
    public partial class MainPage : ContentPage
    {
        readonly FirebaseClient FirebaseClient = new ("https://mauifirebasesilicon-default-rtdb.firebaseio.com/");

        public MainPage()
        {
            InitializeComponent();
        }

        private void NoteDescriptionEditorOnClicked(object sender, EventArgs e)
        {
            if(!IsDescriptionNullOrEmpty())
            {
                FirebaseClient.Child("Notes").PostAsync(new Note
                {
                    Description = NoteDescriptionEditor.Text,
                });
                NoteDescriptionEditor.Text = "";
            }
        }

        private bool IsDescriptionNullOrEmpty()
        {
            return string.IsNullOrEmpty(NoteDescriptionEditor.Text);
        }
    }
}
