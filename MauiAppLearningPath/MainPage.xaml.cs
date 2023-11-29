using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;

namespace MauiAppLearningPath
{
    public partial class MainPage : ContentPage
    {
        readonly FirebaseClient FirebaseClient = new ("https://mauifirebasesilicon-default-rtdb.firebaseio.com/");

        public ObservableCollection<Note> Notes{ get; set; } = new ObservableCollection<Note>();

        public MainPage()
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
                            NoteDescription = NumberToWords(StringToWorkOn);
                        }

                        Notes.Add(new Note
                        {
                            Description = NoteDescription,
                        });
                    }
                });
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

        static string NumberToWords(int numero)
        {
            if (numero < 0 || numero > 1000000)
            {
                return "Número fora da faixa suportada.";
            }

            if (numero == 0)
            {
                return "Zero";
            }

            string[] unidades = { "", "Um", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove" };
            string[] especiaisDezAteDezenove = { "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezesseis", "Dezessete", "Dezoito", "Dezenove" };
            string[] dezenas = { "", "Dez", "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
            string[] centenas = { "", "Cem", "Duzentos", "Trezentos", "Quatrocentos", "Quinhentos", "Seiscentos", "Setecentos", "Oitocentos", "Novecentos" };

            if (numero <= 99)
            {
                return NumberUp99ToWords(numero);
            }
            else if (numero <= 999)
            {
                int centena = numero / 100;
                int resto = numero % 100;
                if (resto == 0)
                {
                    return centenas[centena];
                }
                else
                {
                    return centenas[centena] + " e " + NumberUp99ToWords(resto);
                }
            }
            else
            {
                int milhar = numero / 1000;
                int resto = numero % 1000;
                if (resto == 0)
                {
                    return NumberToWords(milhar) + " Mil";
                }
                else
                {
                    return NumberToWords(milhar) + " Mil e " + NumberToWords(resto);
                }
            }
        }

        static string NumberUp99ToWords(int numero)
        {
            string[] unidades = { "", "Um", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove" };
            string[] especiaisDezAteDezenove = { "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezesseis", "Dezessete", "Dezoito", "Dezenove" };
            string[] dezenas = { "", "Dez", "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };

            if (numero == 0)
            {
                return "";
            }
            else if (numero < 10)
            {
                return unidades[numero];
            }
            else if (numero >= 10 && numero < 20)
            {
                return especiaisDezAteDezenove[numero - 10];
            }
            else
            {
                int unidade = numero % 10;
                int dezena = numero / 10;

                if (unidade == 0)
                {
                    return dezenas[dezena];
                }
                else
                {
                    return dezenas[dezena] + " e " + unidades[unidade];
                }
            }
        }
    }
}
