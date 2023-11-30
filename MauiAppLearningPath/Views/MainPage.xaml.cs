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

        private async void NoteDescriptionEditorOnClicked(object sender, EventArgs e)
        {
            if(!IsDescriptionNullOrEmpty())
            {
                await FirebaseClient.Child("Notes").PostAsync(new Note
                {
                    Description = NoteDescriptionEditor.Text,
                });
                await SendButton.ScaleTo(0.95, 100);
                await SendButton.ScaleTo(1, 100);
                NoteDescriptionEditor.Text = "";
            }
        }

        private bool IsDescriptionNullOrEmpty()
        {
            return string.IsNullOrEmpty(NoteDescriptionEditor.Text);
        }
    }
}
