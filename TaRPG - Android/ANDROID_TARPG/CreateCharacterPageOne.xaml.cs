using Acr.UserDialogs;
using ANDROID_TARPG.Source.Connection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ANDROID_TARPG
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateCharacterPageOne : ContentPage
    {
        private CharacterModel charModel = new CharacterModel();
        private bool rolled = false;
        private Character prevCharacter;
        private List<int> results = new List<int>();
        private bool cancel = false;

        public CreateCharacterPageOne()
        {
            InitializeComponent();
            StandardLoader loader = new StandardLoader();
            loader.LoadStandardValues();
            this.RaceSelector.SetBinding(Picker.ItemsSourceProperty, new Binding() { Source = loader.GetCharRaces() });
            this.ClassSelector.SetBinding(Picker.ItemsSourceProperty, new Binding() { Source = loader.GetCharClasses() });
            rolled = false;
            cancel = false;
            CharacterModel.RolledAttributes = false;
          

        }

        private async void CreateCharacter(object sender, EventArgs e)
        {

            await Navigation.PopAsync();

        }


        private async void cancelCharacter()
        {
            ConfirmConfig config = new ConfirmConfig()
            {
                Message = "Are you sure you want to cancel this character's creation?",
                OkText = "Yes",
                CancelText = "No",
                Title = "Cancel character creation?"

            };
            var result = await UserDialogs.Instance.ConfirmAsync(config);
            if (result)
            {
                CharacterModel.RolledAttributes = false;
                rolled = false;
                cancel = true;
            }

        }
        private void Roll(object sender, EventArgs e)
        {
            RollCharacter();
        }

        private async void Creat(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Strength.Text)|| string.IsNullOrEmpty(Dexterity.Text) || string.IsNullOrEmpty(Constitution.Text) 
                || string.IsNullOrEmpty(Charisma.Text) || string.IsNullOrEmpty(Wisdom.Text) || string.IsNullOrEmpty(Intelligence.Text)
                || string.IsNullOrEmpty(CharacterAge.Text) || string.IsNullOrEmpty(CharacterName.Text) || string.IsNullOrEmpty(CharacterWeight.Text)
                || string.IsNullOrEmpty(CharacterHeightFeet.Text) || string.IsNullOrEmpty(CharacterHeightInches.Text)
                || string.IsNullOrEmpty(RaceSelector.SelectedItem as String) || string.IsNullOrEmpty(ClassSelector.SelectedItem as String))
            {
                DisplayBlankValueWarning();
            }
            else
            {
                var result = await UserDialogs.Instance.ConfirmAsync("Are you sure you want to create this character?", "Confirm character creation", "Yes!", "No, wait!");

                if (result)
                {


                    SetCharacterPhysicalAttributes();
                    CharacterDB.InsertCharacter(CharacterModel.GetCharacterModel);
                    await Navigation.PopToRootAsync();
                }
            }
        }











        private void RollCharacter()
        {
            if (rolled || CharacterModel.RolledAttributes)
            {
                DisplayRollOnceWarning();
                return;
            }

            Random rand = new Random();
            List<int> rolls = new List<int>();
            int sum = 0;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 4; j++)
                    rolls.Add(rand.Next(1, 7));

                rolls.Sort();

                for (int k = 3; k >= 1; k--)
                    sum += rolls.ElementAt(k);

                results.Add(sum);
                rolls.Clear();
                sum = 0;
            }

            Strength.Text = results.ElementAt(0).ToString();
            Dexterity.Text = results.ElementAt(1).ToString();
            Constitution.Text = results.ElementAt(2).ToString();
            Intelligence.Text = results.ElementAt(3).ToString();
            Wisdom.Text = results.ElementAt(4).ToString();
            Charisma.Text = results.ElementAt(5).ToString();
            DisplayAttributeModifiers();

            List<CharAttribute> charAttributes = CreateAttributeList();
            List<int> attributeModifiers = CreateAttributeModifierList();

            if (charAttributes != null && attributeModifiers != null)
            {
                CharacterModel.GetCharacterModel.Attributes = charAttributes;
                CharacterModel.GetCharacterModel.AttributeModifiers = attributeModifiers;
            }

            else
            {
                DisplayInvalidValueWarning();
                return;
            }

            CharacterModel.RolledAttributes = true;
            rolled = true;
            RollAttributesButton.BackgroundColor = Color.LightGray;
        }

        private void DisplayAttributeModifiers()
        {
            StrMod.Text = CalculateModifier(results.ElementAt(0)).ToString();
            DexMod.Text = CalculateModifier(results.ElementAt(1)).ToString();
            ConMod.Text = CalculateModifier(results.ElementAt(2)).ToString();
            IntMod.Text = CalculateModifier(results.ElementAt(3)).ToString();
            WisMod.Text = CalculateModifier(results.ElementAt(4)).ToString();
            ChaMod.Text = CalculateModifier(results.ElementAt(5)).ToString();
        }

        private List<int> CreateAttributeModifierList()
        {
            List<int> modifiers = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                modifiers.Add(CalculateModifier(results.ElementAt(i)));
            }

            return modifiers;
        }
        private List<CharAttribute> CreateAttributeList()
        {
            try
            {
                CharAttribute strength = new CharAttribute("Strength", int.Parse(Strength.Text));
                CharAttribute dexterity = new CharAttribute("Dexterity", int.Parse(Dexterity.Text));
                CharAttribute constitution = new CharAttribute("Constitution", int.Parse(Constitution.Text));
                CharAttribute intelligence = new CharAttribute("Intelligence", int.Parse(Intelligence.Text));
                CharAttribute wisdom = new CharAttribute("Wisdom", int.Parse(Wisdom.Text));
                CharAttribute charisma = new CharAttribute("Charisma", int.Parse(Charisma.Text));

                List<CharAttribute> charAttributes = new List<CharAttribute>();

                charAttributes.Add(strength);
                charAttributes.Add(dexterity);
                charAttributes.Add(constitution);
                charAttributes.Add(intelligence);
                charAttributes.Add(wisdom);
                charAttributes.Add(charisma);

                return charAttributes;
            }

            catch
            {
                return null;
            }
        }


        private int CalculateModifier(int value)
        {
            return (int)Math.Floor((double)(value - 10) / 2);
        }

        private void SetCharacterPhysicalAttributes()
        {
            CharacterModel.GetCharacterModel.Name = CharacterName.Text;

            String selectedClass = ClassSelector.SelectedItem as String;
            String selectedRace = RaceSelector.SelectedItem as String;

            foreach (Class c in StandardLoader.Classes)
            {
                if (selectedClass.Equals(c.Name))
                {
                    CharacterModel.GetCharacterModel.CharacterClass = c;
                    break;
                }
            }

            foreach (Race r in StandardLoader.Races)
            {
                if (selectedRace.Equals(r.Name))
                {
                    CharacterModel.GetCharacterModel.CharacterRace = r;
                    break;
                }
            }

            try
            {
                CharacterModel.GetCharacterModel.Age = int.Parse(CharacterAge.Text);
            }

            catch
            {
                DisplayBlankValueWarning();
            }

            CharacterModel.GetCharacterModel.HeightInFeet = CharacterHeightFeet.Text;
            CharacterModel.GetCharacterModel.HeightInInches = CharacterHeightInches.Text;

            try
            {
                double weightValue = double.Parse(CharacterWeight.Text, CultureInfo.InvariantCulture);
                CharacterModel.GetCharacterModel.Weight = weightValue;
            }

            catch (System.FormatException ex)
            {
                return;
            }
        }

        private async void DisplayBlankValueWarning()
        {
            await UserDialogs.Instance.AlertAsync("One or more fields have invalid values or no values.","Invalid value","Ok");

        }
        private async void DisplayRollOnceWarning()
        {
            await UserDialogs.Instance.AlertAsync("You can only roll attribute values once.", "Already rolled attribute values", "Ok");

        }
        private async void DisplayInvalidValueWarning()
        {
   
            await UserDialogs.Instance.AlertAsync("One of the attribute values is invalid. Make sure the values are numerical and have no spaces or other characters surrounding them.","Invalid value","Ok");
 
        }
    }
}