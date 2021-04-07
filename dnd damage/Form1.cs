using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dnd_damage
{
    public partial class Form1 : Form
    {
        bool firstRoll = true;

        int[] formHeights;

        int yOffset = 5;
        int xOffset = 14;
        int buttonHeight = 23;
        int startButtonYPos = 14;

        List<Button> abilityButtons;

        int[] rollResult = new int[] { 0, 0, 0 };
        int currentAbilityIndex;

        public Form1()
        {
            InitializeComponent();

            TotalController.loadDataFromFile();
            updateCharacterStats();
            generateAbilitiesButtons();
            placeAbilityButtons();
            filComboboxes();
            updateFormHeights();
            updateMainFormSize();

            updateListBoxAbilities();
        }

        public void updateFormHeights()
        {
            formHeights = new int[8];

            formHeights[0] = this.Size.Height;
            formHeights[1] = tabControl1.Height;
            formHeights[2] = panelD20.Height;
            formHeights[3] = panel4.Height;
            formHeights[4] = richTextBoxJournal.Height;
            formHeights[5] = panelAbilitiesButtons.Height;
            formHeights[6] = panelAbilitiesList.Height;
            formHeights[7] = listBoxAbilities.Height;
        }

        public void updateListBoxAbilities()
        {
            listBoxAbilities.Items.Clear();

            for (int i = 0; i < abilityButtons.Count; i++)
            {
                listBoxAbilities.Items.Add(TotalController.getAbilityByIndex(i).Name);
            }

            try
            {
                listBoxAbilities.SelectedIndex = 0;
                openAbility(0);
            }
            catch{}

        }

        public void generateAbilitiesButtons()
        {
            abilityButtons = new List<Button>();

            for (int i = 0; i < TotalController.getAbilities().Count; i++)
            {
                Button button = new Button();
                abilityButtons.Add(button);
            }
        }

        public void placeAbilityButtons()
        {
            Button button;

            for(int i = 0; i < abilityButtons.Count; i++)
            {
                button = abilityButtons[i];
                placeAbilityButton(button, i);
            }
        }

        public void placeAbilityButton(Button button ,int index)
        {
            button.Top = startButtonYPos + (yOffset + buttonHeight) * (index / 2);
            button.Left = (panelAbilitiesButtons.Width / 2) * (index % 2);
            button.Left += (xOffset - (xOffset / 2 * (index % 2)));

            button.Width = panelAbilitiesButtons.Width / 2;
            button.Width -= (int)(xOffset * 1.5);
            button.Height = buttonHeight;


            button.FlatStyle = FlatStyle.Flat;
            button.ForeColor = Color.White;
            setButtonColor(button, TotalController.getAbilityDamageType(index));
            button.Name = Convert.ToString(index);
            button.Text = TotalController.getAbilityName(index);
            button.Click += new EventHandler(abilityButton_Click);
            panelAbilitiesButtons.Controls.Add(button);
        }

        public void setButtonColor(Button button,DamageTypes damageType)
        {
            switch (damageType)
            {
                case DamageTypes.acid: button.BackColor = AbilityColors.acid; break;
                case DamageTypes.bludgeoning: button.BackColor = AbilityColors.bludgeoning; break;
                case DamageTypes.cold: button.BackColor = AbilityColors.cold; break;
                case DamageTypes.fire: button.BackColor = AbilityColors.fire; break;
                case DamageTypes.force: button.BackColor = AbilityColors.force; break;
                case DamageTypes.lightning: button.BackColor = AbilityColors.lightning; break;
                case DamageTypes.necrotic: button.BackColor = AbilityColors.necrotic; break;
                case DamageTypes.piercing: button.BackColor = AbilityColors.piercing; break;
                case DamageTypes.poison: button.BackColor = AbilityColors.poison; break;
                case DamageTypes.psychiс: button.BackColor = AbilityColors.psychis; break;
                case DamageTypes.radiant: button.BackColor = AbilityColors.radiant; break;
                case DamageTypes.slashing: button.BackColor = AbilityColors.slashing; break;
                case DamageTypes.thunder: button.BackColor = AbilityColors.thunder; break;
                case DamageTypes.healing: button.BackColor = AbilityColors.healing; break;
            }
        }

        private void abilityButton_Click(object sender, EventArgs e)
        {

            for (currentAbilityIndex = 0; currentAbilityIndex < abilityButtons.Count; currentAbilityIndex++)
            {
                if (sender == abilityButtons[currentAbilityIndex])
                {
                    updateTextboxDamageInDetail(currentAbilityIndex);
                    updateTextboxDamage(currentAbilityIndex);
                }
            }
        }

        private void addToJournal(int i)
        {
            Damage[] damageSums = TotalController.getDamageSumInDetail(i);
            int sumRollD20 = rollResult[0] + rollResult[1] + rollResult[2];

            richTextBoxJournal.AppendText("попадание " + sumRollD20 + " (" + rollResult[0] + ") ");

            if(damageSums.Length != 0)
            {
                richTextBoxJournal.AppendText("\t   урон(" + TotalController.getDamageSum() + ") \t расписанный урон ");

                for (int j = 0; j < damageSums.Length; j++)
                {
                    if (richTextBoxJournal.Text != "")
                        richTextBoxJournal.AppendText("+");

                    changeRichTextBoxBackColor(richTextBoxJournal, damageSums[j].Type);
                    richTextBoxJournal.AppendText(Convert.ToString(damageSums[j].DamageValue));
                    richTextBoxJournal.SelectionBackColor = Color.FromArgb(60, 62, 68);
                }
            }

            else
            {
                richTextBoxJournal.AppendText("\t\tпромах");
            }
            richTextBoxJournal.AppendText("\t");
            richTextBoxJournal.AppendText(DateTime.Now.ToString("HH:mm:ss"));
            richTextBoxJournal.AppendText("\n");

            if (firstRoll)
            {
                richTextBoxJournal.Clear();
                firstRoll = false;
            }
        }

        private void updateTextboxDamage(int i)
        {
            richTextBoxDamage.Clear();

            Damage[] damageSums = TotalController.getDamageSumInDetail(i);

            for (int j = 0; j < damageSums.Length; j++)
            {
                if (richTextBoxDamage.Text != "")
                    richTextBoxDamage.AppendText("+");

                changeRichTextBoxBackColor(richTextBoxDamage, damageSums[j].Type);
                richTextBoxDamage.AppendText(Convert.ToString(damageSums[j].DamageValue));
                richTextBoxDamage.SelectionBackColor = Color.FromArgb(60, 62, 68);
            }

            richTextBoxDamageSum.Text = (Convert.ToString(TotalController.getDamageSum()));

        }

        private void updateTextboxDamageInDetail(int i)
        {
            TotalController.useAbility(i);
            int[] damage = TotalController.getDamageInDetail(i);

            changeRichTextBoxBackColor(richTextBoxDamageInDetail, TotalController.getAbilityDamageType(i));

            for (int j = 0; j < damage.Length; j++)
            {
                if (richTextBoxDamageInDetail.Text != "")
                    richTextBoxDamageInDetail.AppendText("+");

                richTextBoxDamageInDetail.AppendText(Convert.ToString(damage[j]));
            }

            if (TotalController.getAbilityPlusDamage(i) != 0)
                richTextBoxDamageInDetail.AppendText("+" + TotalController.getAbilityPlusDamage(i));
        }

        private void changeRichTextBoxBackColor(RichTextBox richTextBox, DamageTypes damageType)
        {
            switch (damageType)
            {
                case DamageTypes.acid: richTextBox.SelectionBackColor = AbilityColors.acid; break;
                case DamageTypes.bludgeoning: richTextBox.SelectionBackColor = AbilityColors.bludgeoning; break;
                case DamageTypes.cold: richTextBox.SelectionBackColor = AbilityColors.cold; break;
                case DamageTypes.fire: richTextBox.SelectionBackColor = AbilityColors.fire; break;
                case DamageTypes.force: richTextBox.SelectionBackColor = AbilityColors.force; break;
                case DamageTypes.lightning: richTextBox.SelectionBackColor = AbilityColors.lightning; break;
                case DamageTypes.necrotic: richTextBox.SelectionBackColor = AbilityColors.necrotic; break;
                case DamageTypes.piercing: richTextBox.SelectionBackColor = AbilityColors.piercing; break;
                case DamageTypes.poison: richTextBox.SelectionBackColor = AbilityColors.poison; break;
                case DamageTypes.psychiс: richTextBox.SelectionBackColor = AbilityColors.psychis; break;
                case DamageTypes.radiant: richTextBox.SelectionBackColor = AbilityColors.radiant; break;
                case DamageTypes.slashing: richTextBox.SelectionBackColor = AbilityColors.slashing; break;
                case DamageTypes.thunder: richTextBox.SelectionBackColor = AbilityColors.thunder; break;
                case DamageTypes.healing: richTextBox.SelectionBackColor = AbilityColors.healing; break;
            }
        }

        public void filComboboxes()
        {
            string[] modList = new string[] { "+сила", "+ловкость", "+выносливость", "+интеллект", "+мудрость", "+харизма" };

            string[] damageTypeList = new string[] { 
                "рубящий", "колющий", "дробящий", "кислотный", 
                "холодный", "огненный", "силовой", "электрический", 
                "некротический", "ядовитый", "психический", "излучение", "звуковой",
                "исцеление"
            };

            comboBoxModScale.Items.AddRange(modList);
            comboBoxModScale.SelectedItem = comboBoxModScale.Items[TotalController.getCurrentAbilityIndex()];
            comboBoxModScale.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBoxDamageType.Items.AddRange(damageTypeList);
            comboBoxDamageType.SelectedItem = comboBoxDamageType.Items[0];
            comboBoxDamageType.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        public void updateMainFormSize()
        {
            if (abilityButtons.Count > 8)
            {
                this.Size = new Size(this.Size.Width, formHeights[0] + ((abilityButtons.Count - 10 + 1) / 2 * (buttonHeight + yOffset)));
                tabControl1.Size = new Size(tabControl1.Size.Width, formHeights[1] + ((abilityButtons.Count - 10 + 1) / 2 * (buttonHeight + yOffset)));
                panelD20.Size = new Size(panelD20.Size.Width, formHeights[2] + ((abilityButtons.Count - 10 + 1) / 2 * (buttonHeight + yOffset)));
                panel4.Size = new Size(panel4.Size.Width, formHeights[3] + ((abilityButtons.Count - 10 + 1) / 2 * (buttonHeight + yOffset)));
                richTextBoxJournal.Size = new Size(richTextBoxJournal.Size.Width, formHeights[4] + ((abilityButtons.Count - 10 + 1) / 2 * (buttonHeight + yOffset)));
                panelAbilitiesButtons.Size = new Size(panelAbilitiesButtons.Size.Width, formHeights[5] + ((abilityButtons.Count - 10 + 1) / 2 * (buttonHeight + yOffset)));
                panelAbilitiesList.Size = new Size(panelAbilitiesList.Size.Width, formHeights[6] + ((abilityButtons.Count - 10 + 1) / 2 * (buttonHeight + yOffset)));
                listBoxAbilities.Size = new Size(listBoxAbilities.Size.Width, formHeights[7] + ((abilityButtons.Count - 10 + 1) / 2 * (buttonHeight + yOffset)));
            }
        }

        private void buttonD20_Click(object sender, EventArgs e)
        {
            addToJournal(currentAbilityIndex);
            clearDamageTextboxes();
            rollResult = TotalController.rollD20();
            updateD20TextBoxes(rollResult[0], rollResult[1], rollResult[2]);
            TotalController.resetCalculator();
        }

        private void buttonD20witAdvantage_Click(object sender, EventArgs e)
        {
            addToJournal(currentAbilityIndex);
            clearDamageTextboxes();
            rollResult = TotalController.rollD20withAdvantage();
            updateD20TextBoxes(rollResult[0], rollResult[1], rollResult[2]);
            TotalController.resetCalculator();
        }

        private void buttonD20withHindrance_Click(object sender, EventArgs e)
        {
            addToJournal(currentAbilityIndex);
            clearDamageTextboxes();
            rollResult = TotalController.rollD20withHindrance();
            updateD20TextBoxes(rollResult[0], rollResult[1], rollResult[2]);
            TotalController.resetCalculator();
        }

        private void clearDamageTextboxes()
        {
            richTextBoxDamage.Clear();
            richTextBoxDamageInDetail.Clear();
            richTextBoxDamageSum.Clear();
        }

        private void updateD20TextBoxes(int rollResult, int mod, int pb)
        {
            richTextBoxD20.Text = Convert.ToString(rollResult + mod + pb);
            richTextBoxD20InDetail.Text = (rollResult + " + мод " + mod + " + бма " + pb);

            if (rollResult == 1)
                richTextBoxD20.AppendText(" (фэйл)");

            if (rollResult == 20)
                richTextBoxD20.AppendText(" (крит)");
        }

        private void buttonHitNotRequired_Click(object sender, EventArgs e)
        {
            addToJournal(currentAbilityIndex);
            clearDamageTextboxes();
            rollResult = new int[] { 100, 0, 0 };

            richTextBoxD20.Text = Convert.ToString("успех");
            richTextBoxD20InDetail.Text = ("успех");

            TotalController.resetCalculator();
        }

        private void buttonClearJournal_Click(object sender, EventArgs e)
        {
            richTextBoxJournal.Clear();
        }

        public void openAbility(int index)
        {
            textBoxAbilityName.Text = TotalController.getAbilityByIndex(index).Name;
            textBoxDiceQuantity.Text = Convert.ToString(TotalController.getAbilityByIndex(index).DiceQuantity);
            textBoxDiceDamage.Text = Convert.ToString(TotalController.getAbilityByIndex(index).DiceDamage);
            textBoxPlusDamage.Text = Convert.ToString(TotalController.getAbilityByIndex(index).PlusDamage);

            /*switch (AbilitiesManager.abilities[index].DamageType)
            {
                case DamageTypes.acid: 
            }*/

            comboBoxDamageType.SelectedItem = comboBoxDamageType.Items[Convert.ToInt32(TotalController.getAbilityByIndex(index).DamageType)];
            //comboBoxDamageType.SelectedItem = 3;

        }

        private void listBoxAbilities_SelectedIndexChanged(object sender, EventArgs e)
        {
            openAbility(listBoxAbilities.SelectedIndex);
        }

        private void buttonCreateAbility_Click(object sender, EventArgs e)
        {
            if (textBoxAbilityName.Text != "" &&
                textBoxDiceDamage.Text != "" &&
                textBoxDiceQuantity.Text != "" &&
                textBoxPlusDamage.Text != "")
            {
                try
                {
                    TotalController.addAbility(new Ability(
                        textBoxAbilityName.Text,
                        (DamageTypes)comboBoxDamageType.SelectedIndex,
                        Convert.ToInt32(textBoxDiceQuantity.Text),
                        Convert.ToInt32(textBoxDiceDamage.Text),
                        Convert.ToInt32(textBoxPlusDamage.Text))
                        );

                    Button button = new Button();
                    abilityButtons.Add(button);
                    placeAbilityButton(button, abilityButtons.Count - 1);
                    updateListBoxAbilities();
                    updateMainFormSize();
                    clearDamageTextboxes();

                    TotalController.saveDataToFile();
                }
                catch
                {
                    MessageBox.Show("ошибка ввода");
                }

            }

            else MessageBox.Show("все поля должны быть заполнены");
        }

        private void buttonChangeAbility_Click(object sender, EventArgs e)
        {
            if (listBoxAbilities.SelectedIndex != -1)
            {
                if (textBoxAbilityName.Text != "" &&
                    textBoxDiceDamage.Text != "" &&
                    textBoxDiceQuantity.Text != "" &&
                    textBoxPlusDamage.Text != "")
                {
                    try
                    {
                        int index = listBoxAbilities.SelectedIndex;
                        TotalController.getAbilityByIndex(index).Name = textBoxAbilityName.Text;
                        TotalController.getAbilityByIndex(index).DiceQuantity = Convert.ToInt32(textBoxDiceQuantity.Text);
                        TotalController.getAbilityByIndex(index).DiceDamage = Convert.ToInt32(textBoxDiceDamage.Text);
                        TotalController.getAbilityByIndex(index).PlusDamage = Convert.ToInt32(textBoxPlusDamage.Text);
                        TotalController.getAbilityByIndex(index).DamageType = (DamageTypes)comboBoxDamageType.SelectedIndex;

                        abilityButtons[listBoxAbilities.SelectedIndex].Text = TotalController.getAbilityByIndex(index).Name;
                        setButtonColor(abilityButtons[listBoxAbilities.SelectedIndex], TotalController.getAbilityByIndex(index).DamageType);
                        updateListBoxAbilities();

                        TotalController.saveDataToFile();
                    }
                    catch
                    {
                        MessageBox.Show("ошибка ввода");
                    }
                }

                else MessageBox.Show("все поля должны быть заполнены");
            }

            else MessageBox.Show("нет способностей, которые можно было бы изменить");

        }

        private void buttonRemoveAbility_Click(object sender, EventArgs e)
        {
            try
            {
                TotalController.removeAbilityByIndex(listBoxAbilities.SelectedIndex);
                panelAbilitiesButtons.Controls.Remove(abilityButtons[listBoxAbilities.SelectedIndex]);
                abilityButtons.RemoveAt(listBoxAbilities.SelectedIndex);
                placeAbilityButtons();
                updateListBoxAbilities();
                updateMainFormSize();

                TotalController.saveDataToFile();
            }
            catch { }

        }

        private void comboBoxModScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeCurrentStat();
            TotalController.saveDataToFile();
        }

        private void changeCurrentStat()
        {
            TotalController.changeCharacterCurrentStat(comboBoxModScale.SelectedIndex);
        }

        private void buttonSaveCharacterChanges_Click(object sender, EventArgs e)
        {
            if (textBoxPBmod.Text != "" &&
                textBoxSTRmod.Text != "" &&
                textBoxDEXmod.Text != "" &&
                textBoxCONmod.Text != "" &&
                textBoxINTmod.Text != "" &&
                textBoxWISmod.Text != "" &&
                textBoxCHAmod.Text != "")
            {
                try
                {
                    int[] newStats = new int[]
                    {
                        Convert.ToInt32(textBoxPBmod.Text),
                        Convert.ToInt32(textBoxSTRmod.Text),
                        Convert.ToInt32(textBoxDEXmod.Text),
                        Convert.ToInt32(textBoxCONmod.Text),
                        Convert.ToInt32(textBoxINTmod.Text),
                        Convert.ToInt32(textBoxWISmod.Text),
                        Convert.ToInt32(textBoxCHAmod.Text)
                    };

                    TotalController.setCharacterStats(newStats);

                    changeCurrentStat();

                    TotalController.saveDataToFile();
                }
                catch
                {
                    MessageBox.Show("ошибка ввода");
                }
            }

            else MessageBox.Show("все поля должны быть заполнены");
        }

        private void updateCharacterStats()
        {

            int[] newStats = TotalController.getCharacterStats();
            textBoxPBmod.Text = Convert.ToString(newStats[0]);
            textBoxSTRmod.Text = Convert.ToString(newStats[1]);
            textBoxDEXmod.Text = Convert.ToString(newStats[2]);
            textBoxCONmod.Text = Convert.ToString(newStats[3]);
            textBoxINTmod.Text = Convert.ToString(newStats[4]);
            textBoxWISmod.Text = Convert.ToString(newStats[5]);
            textBoxCHAmod.Text = Convert.ToString(newStats[6]);
        }

        private void buttonClearAbilityTextboxes_Click(object sender, EventArgs e)
        {
            textBoxAbilityName.Clear();
            textBoxDiceQuantity.Clear();
            textBoxDiceDamage.Clear();
            textBoxPlusDamage.Clear();
        }
    }
}
