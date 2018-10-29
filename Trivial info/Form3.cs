using System;
using System.Windows.Forms;

namespace Trivial_info
{
    public partial class Form3 : Form
    {
        //propriétés:
        string[] page = new string[2];
        int numPage = 0;

        public Form3()
        {
            InitializeComponent();

            page[0] = "Objectif : Atteindre la case 'Arrivée' avant le second Joueur. \r\n"
                    + "\r\nComment jouer : \r\n"
                    + "\r\n  - Première question: vous devez remettre dans l'ordre les réponses pour déterminer votre position de départ sur la plateau. \r\n"
                    + "\r\n  - Les autres questions: deux solutions s'offrent à vous : \r\n"
                    + "     - En répondant juste, vous avancerez de 2 ou 3 cases \r\n"
                    + "     - En répondant faux, vous reculerez de 2 cases \r\n";

            page[1] = " Les pièges à éviter: \r\n"
                    + "   - Attention au temps : pour chaque question (sauf la première), vous disposez de 20 secondes pour répondre. \r\n"
                    + "   - Attention aux cases piégées : certaines cases vous réservent des surprises, il vous faudra parfois compter sur la chance pour vous en sortir ! \r\n"
                    + "   - Attention aux questions qui déstabilisent : certaines questions seront modifiées de temps en temps, ne vous laissez pas déstabiliser ! \r\n"
                    + "\r\n Enfin, lorsque vous répondez juste plus de 2 fois de suite, une des réponses sera masquée. Cela peut être une bonne comme une mauvaise réponse... "
                    + "\r\n \r\n                                     Bonne chance ! ";

            btn_gauche.Enabled = false;

            txt_regles.Text = page[0];
            lbl_numPage.Text = "Page " + Convert.ToString(numPage+1);
        }

        private void btn_fermer_Click(object sender, EventArgs e)
        {
            //fonction bugguée! Ne pas modifier!
        }

        private void btn_quitter_Click(object sender, EventArgs e)
        {
            Form2.ActiveForm.Show();
            this.Close();
        }

        private void btn_gauche_Click(object sender, EventArgs e)
        {
            numPage--;
            txt_regles.Text = page[numPage];
            lbl_numPage.Text = "Page " + Convert.ToString(numPage+1);
            if (numPage <= 0)
                btn_gauche.Enabled = false;
            if (numPage < 1)
                btn_droite.Enabled = true;
        }

        private void btn_droite_Click(object sender, EventArgs e)
        {
            numPage++;
            txt_regles.Text = page[numPage];
            lbl_numPage.Text = "Page " + Convert.ToString(numPage+1);
            if (numPage >= 1)
                btn_droite.Enabled = false;
            if (numPage > 0)
                btn_gauche.Enabled = true;
        }
    }
}
