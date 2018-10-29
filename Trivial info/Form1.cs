using System;
using System.Drawing;
using System.Windows.Forms;

namespace Trivial_info
{
    public partial class Form1 : Form
    {
        //propriétés:
        public bool modeBug = false;
        public bool jeuChronometre = false;

        Random rnd = new Random();

        int categQuestion = 0; //catégorie de la question à poser: dépend de la couleur de la case!
        string question = "";   //question qui sera placée dans le label "question"
        string repA = "", repB = "", repC = "", repD = "";  //les 4 réponses, stockées dans les labels repA, repB, repC, et repD
       
        enum bonneRep {A=1, B=2, C=3, D=4};     //correspond aux 4 boutons A, B, C, et D, 
                                                //parmi lesquels on choisira la bonne réponse!
        int bonneReponse = 0;

        personnage p1, p2;  //les 2 personnages!
        Label[] cases;  // toutes les cases du plateau;
        int numJoueur = 1; // le joueur qui joue!
        int chrono = 210; // le chrono du timer1
        int brepj1 = 0, brepj2 = 0; // nombre de bonnes réponses à la suite
        int numQuestion = 0;    //numéro de la question générée aléatoirement
        int numQuestionBug = 0; //correspond au numero de la question qui sera bugguée!
        public bool reponsesBug = false;
        int xa, ya, xb, yb, xc, yc, xd, yd; // les coordonnées des 4 boutons A, B, C, D qui seront modifiées dans le bug!
        int vitXa, vitYa, vitXb, vitYb, vitXc, vitYc, vitXd, vitYd;
        int xaOr, yaOr, xbOr, ybOr, xcOr, ycOr, xdOr, ydOr; //correspondent aux coordonnées de départ des 4 boutons A, B, C, D
        int case_pass_tr1, case_pass_tr2, case_pass_tr3; // cases passage de tour
        bool pass_tr_J1, pass_tr_J2; // cases passage de tour
        int pass_tr; // cases passage de tour
        int case_ret_dep_1J, case_ret_dep_2J; // cases retour à la première case du plateau

        //variables pour le départ:
        bool departJ1 = true;
        bool departJ2 = true;
        bool btn1select = false;
        bool btn2select = false;
        bool btn3select = false;
        bool btn4select = false;
        int erepA, erepB, erepC, erepD;
        int brepA, brepB, brepC, brepD;

        //constructeurs:
        public Form1(bool modeBugf2, bool chronof2)
        {
            InitializeComponent();

            modeBug = modeBugf2;
            jeuChronometre = chronof2;

            xaOr = btn_repA.Location.X;
            yaOr = btn_repA.Location.Y;
            xbOr = btn_repB.Location.X;
            ybOr = btn_repB.Location.Y;
            xcOr = btn_repC.Location.X;
            ycOr = btn_repC.Location.Y;
            xdOr = btn_repD.Location.X;
            ydOr = btn_repD.Location.Y;

            xa = xaOr;
            ya = yaOr;
            xb = xbOr;
            yb = ybOr;
            xc = xcOr;
            yc = ycOr;
            xd = xdOr;
            yd = ydOr;

            vitXa = rnd.Next(3) + 1;
            vitYa = rnd.Next(3) + 1;
            vitXb = rnd.Next(3) + 1;
            vitYb = rnd.Next(3) + 1;
            vitXc = rnd.Next(3) + 1;
            vitYc = rnd.Next(3) + 1;
            vitXd = rnd.Next(3) + 1;
            vitYd = rnd.Next(3) + 1;

            //on initialise les 64 cases des labels:
            cases = new Label[64];
            cases[0] = lbl_Depart;
            cases[1] = lbl1;
            cases[2] = lbl2;
            cases[3] = lbl3;
            cases[4] = lbl4;
            cases[5] = lbl5;
            cases[6] = lbl6;
            cases[7] = lbl7;
            cases[8] = lbl8;
            cases[9] = lbl9;
            cases[10] = lbl10;
            cases[11] = lbl11;
            cases[12] = lbl12;
            cases[13] = lbl13;
            cases[14] = lbl14;
            cases[15] = lbl15;
            cases[16] = lbl16;
            cases[17] = lbl17;
            cases[18] = lbl18;
            cases[19] = lbl19;
            cases[20] = lbl20;
            cases[21] = lbl21;
            cases[22] = lbl22;
            cases[23] = lbl23;
            cases[24] = lbl24;
            cases[25] = lbl25;
            cases[26] = lbl26;
            cases[27] = lbl27;
            cases[28] = lbl28;
            cases[29] = lbl29;
            cases[30] = lbl30;
            cases[31] = lbl31;
            cases[32] = lbl32;
            cases[33] = lbl33;
            cases[34] = lbl34;
            cases[35] = lbl35;
            cases[36] = lbl36;
            cases[37] = lbl37;
            cases[38] = lbl38;
            cases[39] = lbl39;
            cases[40] = lbl40;
            cases[41] = lbl41;
            cases[42] = lbl42;
            cases[43] = lbl43;
            cases[44] = lbl44;
            cases[45] = lbl45;
            cases[46] = lbl46;
            cases[47] = lbl47;
            cases[48] = lbl48;
            cases[49] = lbl49;
            cases[50] = lbl50;
            cases[51] = lbl51;
            cases[52] = lbl52;
            cases[53] = lbl53;
            cases[54] = lbl54;
            cases[55] = lbl55;
            cases[56] = lbl56;
            cases[57] = lbl57;
            cases[58] = lbl58;
            cases[59] = lbl59;
            cases[60] = lbl60;
            cases[61] = lbl61;
            cases[62] = lbl62;
            cases[63] = lblArrivee;

            timer1.Start(); // démarrage du timer
            p1 = new personnage(cases[0], lbl_player1);
            p2 = new personnage(cases[0], lbl_player2);
            //on décale le joueur 2 de quelques pixels pour éviter une superposition parfaite:
            lbl_player2.Location = new Point(lbl_player2.Location.X, lbl_player2.Location.Y + 10);

            //On choisira une ou plusieurs questions de base pour le choix du Joueur qui jouera en premier!
            question = "";
            repA = "";
            repB = "";
            repC = "";
            repD = "";

            lbl_question.Text = question;
            lbl_repA.Text = repA;
            lbl_repB.Text = repB;
            lbl_repC.Text = repC;
            lbl_repD.Text = repD;

            // initialisation des cases de passage de tour
            case_pass_tr1 = 11+rnd.Next(16);
            case_pass_tr2 = 27+rnd.Next(16);
            case_pass_tr3 = 43+rnd.Next(14);
            pass_tr_J1 = false;
            pass_tr_J2 = false;
            pass_tr = 0;

            // initialisation des cases de retour à la première case du plateau
            do{
                case_ret_dep_1J = 11 + rnd.Next(46);
            }while (case_ret_dep_1J==case_pass_tr1 || case_ret_dep_1J==case_pass_tr2 || case_ret_dep_1J==case_pass_tr3);

            do
            {
                case_ret_dep_2J = 11 + rnd.Next(46);
            } while (case_ret_dep_1J == case_ret_dep_2J || case_ret_dep_2J == case_pass_tr1 || case_ret_dep_2J == case_pass_tr2 || case_ret_dep_2J == case_pass_tr3);

            //pour le moment: joueur par defaut = joueur 1, position = case[1]:
            //initialisation du joueur qui commence:
            numJoueur = rnd.Next(2) + 1;

            lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();

            //on commence avec le J1:
            p1.numLabEnCours = 0;
            lbl_player1.Location = new Point(cases[p1.numLabEnCours].Location.X+cases[p1.numLabEnCours].Width/6, cases[p1.numLabEnCours].Location.Y+cases[p1.numLabEnCours].Height/4);
            lbl_player1.BackColor = cases[p1.numLabEnCours].BackColor;
            p1.lblEnCours = cases[p1.numLabEnCours];
            if (departJ1 == true)
            {
                init();
                lblChrono.Visible = false;
            }

            //on commence avec le J2:
            p2.numLabEnCours = 0;
            lbl_player2.Location = new Point(cases[p2.numLabEnCours].Location.X + cases[p2.numLabEnCours].Width / 6, cases[p2.numLabEnCours].Location.Y+cases[p2.numLabEnCours].Height/4);
            lbl_player2.BackColor = cases[p2.numLabEnCours].BackColor;
            p2.lblEnCours = cases[p2.numLabEnCours];
            if (departJ2 == true)
            {
                init();
                lblChrono.Visible = false;
            }
            
        }
       
        /************************************************************************************************/

        //les class:
        class personnage
        {
            //propriétés:
            public Label lblEnCours;
            public int numLabEnCours; // numero du label parcouru.
            public bool bonneReponse;
            public bool gagner;
            private Random rand = new Random();

            //constructeur:
            public personnage(Label lbl, Label lblPlayer)
            {
                numLabEnCours = 0;
                lblEnCours = lbl;
                bonneReponse = false;
                lblPlayer.Location = lbl.Location;
                gagner = false;
            }

            //methodes:
            //deplacement du joueur:
            public void deplPerso(bool reponse)
            {
                //la reponse est bonne:
                if (reponse)
                {
                    //position sur le plateau:
                    if (numLabEnCours >= 61)
                    {
                        //victoire:
                        numLabEnCours = 63;
                    }
                    else
                    {
                        numLabEnCours += rand.Next(2) + 2;
                    }
                }
                //la reponse n'est pas bonne:
                else
                {
                    //position sur le plateau:
                    if (numLabEnCours <= 2)
                    {
                        //remise sur la case 1:
                        numLabEnCours = 1;
                    }
                    else
                    {
                        numLabEnCours-=2;
                    }
                }
            }                        
        }

        /************************************************************************************************/

        //les methodes:
        //récupération de la catégorie de la question, en fonction de la couleur de la case:
        public void recupererCategorie(Color c)
        {
            if (c == Color.DarkOrange)
                categQuestion = 0;
            else if (c == Color.Lime)
                categQuestion = 1;
            else if (c == Color.FromArgb(255,128,255))
                categQuestion = 2;
            else if (c == Color.Fuchsia)
                categQuestion = 3;
            else if (c == Color.Aqua)
                categQuestion = 4;
            else if (c == Color.Yellow)
                categQuestion = 5;
        }
        
        //fonction pour générer des bugs sur une question:
        public void bugQuestion1(Label Q)
        {
            Label Question = Q;
            char voyelle = 'a';
            int alea = 0;
                //on parcours chaque lettre de la question, et on change les voyelles:
                for (int i = 0; i < Question.Text.Length; i++)
                {
                    if (Question.Text[i] == 'a' || Question.Text[i] == 'o' || Question.Text[i] == 'i' || Question.Text[i] == 'u' || Question.Text[i] == 'y' || Question.Text[i] == 'e' || Question.Text[i] == 'é' || Question.Text[i] == 'è' || Question.Text[i] == 'à')
                    {
                        alea = rnd.Next(6);
                        string s = Question.Text;
                        switch (alea)
                        {
                            case 0: voyelle = 'a'; break;
                            case 1: voyelle = 'e'; break;
                            case 2: voyelle = 'y'; break;
                            case 3: voyelle = 'u'; break;
                            case 4: voyelle = 'i'; break;
                            case 5: voyelle = 'o'; break;
                        }
                        string s2 = s.Replace(Question.Text[i], voyelle);
                        Question.Text = s2;
                    }
                }
                lbl_question.Text = Question.Text;
        }

        //fonction pour déplacer les boutons de réponses de façon aléatoire (bug 2):
        public void bugQuestion2()
        {
            xa += vitXa;
            ya += vitYa;
            xb -= vitXb;
            yb += vitYb;
            xc += vitXc;
            yc -= vitYc;
            xd -= vitXd;
            yd -= vitYd;
            if (xa + btn_repA.Width >= this.ClientSize.Width || xa < 0)
                vitXa *= -1;
            if (ya + btn_repA.Height >= this.ClientSize.Height || ya < 0)
                vitYa *= -1;
            if (xb + btn_repB.Width >= this.ClientSize.Width || xb < 0)
                vitXb *= -1;
            if (yb + btn_repB.Height >= this.ClientSize.Height || yb < 0)
                vitYb *= -1;
            if (xc + btn_repC.Width >= this.ClientSize.Width || xc < 0)
                vitXc *= -1;
            if (yc + btn_repC.Height >= this.ClientSize.Height || yc < 0)
                vitYc *= -1;
            if (xd + btn_repD.Width >= this.ClientSize.Width || xd < 0)
                vitXd *= -1;
            if (yd + btn_repD.Height >= this.ClientSize.Height || yd < 0)
                vitYd *= -1;
            btn_repA.Location = new Point(xa, ya);
            btn_repB.Location = new Point(xb, yb);
            btn_repC.Location = new Point(xc, yc);
            btn_repD.Location = new Point(xd, yd);
        }

        //quand on a répondu avec le bug 2, on replace les boutons à leur place d'origine:
        public void replaceBtnRep()
        {
            xa = xaOr;
            ya = yaOr;
            xb = xbOr;
            yb = ybOr;
            xc = xcOr;
            yc = ycOr;
            xd = xdOr;
            yd = ydOr;
            vitXa = rnd.Next(3) + 1;
            vitYa = rnd.Next(3) + 1;
            vitXb = rnd.Next(3) + 1;
            vitYb = rnd.Next(3) + 1;
            vitXc = rnd.Next(3) + 1;
            vitYc = rnd.Next(3) + 1;
            vitXd = rnd.Next(3) + 1;
            vitYd = rnd.Next(3) + 1;
            btn_repA.Location = new Point(xaOr, yaOr);
            btn_repB.Location = new Point(xbOr, ybOr);
            btn_repC.Location = new Point(xcOr, ycOr);
            btn_repD.Location = new Point(xdOr, ydOr);
        }

        //on choisira une question en fonction de la catégorie de la question:
        public void genererQuestion(int categorie)
        {
            int repdisparue = rnd.Next(4);
            numQuestionBug = rnd.Next(10);

            if (modeBug == false)
            {
                chrono = 210;
            }
            else
            {
                chrono = 310;
            }

            switch (categorie)
            {
                case 0: // sécurité
                    //on choisit une question aléatoire, ainsi que les réponses correspondantes parmi la catégorie selectionnée:
                    numQuestion = rnd.Next(11);
                    switch (numQuestion)
                    {
                        case 0: lbl_question.Text = "Quel type de logiciel est dédié à la protection de votre ordinateur contre les virus ?";
                                lbl_repA.Text = "Un anti-virus";
                                lbl_repB.Text = "Un firewall";
                                lbl_repC.Text = "Un TCP/IP";
                                lbl_repD.Text = "Un vaccin ?";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 1: lbl_question.Text = "Que faut-il faire pour empêcher une personne d'accéder à votre ordinateur ?";
                                lbl_repA.Text = "Le frapper";
                                lbl_repB.Text = "Mettre un mot de passe";
                                lbl_repC.Text = "Planquer son PC";
                                lbl_repD.Text = "Ne pas emmener son pc";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 2: lbl_question.Text = "A quoi correspond le firewall ?";
                                lbl_repA.Text = "Le pare-feu";
                                lbl_repB.Text = "Un site contre les murs de feu";
                                lbl_repC.Text = "Un anti-virus";
                                lbl_repD.Text = "Rien";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 3: lbl_question.Text = "Sur votre compte e-mail, quelle est la règle numéro 1 à respecter ?";
                                lbl_repA.Text = "Donner son mot de passe à un ami";
                                lbl_repB.Text = "Ne donner à personne son mot de passe";
                                lbl_repC.Text = "Envoyer des e-mails à des personnes";
                                lbl_repD.Text = "Ne pas avoir d'adresse e-mail";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 4: lbl_question.Text = "Si vous recevez un e-mail d'un inconnu qui prétend vous aimer, que faites-vous ?";
                                lbl_repA.Text = "Je l'ouvre sans hésiter";
                                lbl_repB.Text = "Je supprime cet e-mail sans le consulter";
                                lbl_repC.Text = "Je l'ouvre pour voir les pièces jointes";
                                lbl_repD.Text = "Je lui réponds que je l'aime aussi";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 5: lbl_question.Text = "Certains programmes ne se lancent pas avec certaines sessions pourquoi ?";
                                lbl_repA.Text = "Ces programmes ne sont pas au point";
                                lbl_repB.Text = "Ces programmes sont limités d'utilisation en temps";
                                lbl_repC.Text = "Ces programmes ne peuvent être exécutés qu'en mode administrateur";
                                lbl_repD.Text = "Ces programmes sont mal installés";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 6: lbl_question.Text = "Vous devez acheter un nouveau chargeur car l'ancien a grillé, que devez-vous faire pour ne pas détruire votre ordinateur ?";
                                lbl_repA.Text = "Garder l'ancien boîtier et acheter une prise pour brancher à un secteur";
                                lbl_repB.Text = "Vérifier l'ampérage ainsi que le point d'accès avant d'acheter un nouveau chargeur";
                                lbl_repC.Text = "Changer la batterie de l'ordinateur";
                                lbl_repD.Text = "Acheter n'importe quel chargeur";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 7: lbl_question.Text = "En tant qu'administrateur, quels droits faut-il donner aux utilisateurs standards ?";
                                lbl_repA.Text = "Les droits de lecture seule sur les dossiers partagés";
                                lbl_repB.Text = "Les droits d'administration";
                                lbl_repC.Text = "Le droit de contrôle total";
                                lbl_repD.Text = "Les droits de lecture et d'écriture sur les dossiers partagés";
                                bonneReponse = (int)bonneRep.D;
                            break;

                        case 8: lbl_question.Text = "Lorsqu'on démonte un ordinateur, quel type de vêtements est-il recommandé de porter ?";
                                lbl_repA.Text = "Du synthétique";
                                lbl_repB.Text = "Du coton";
                                lbl_repC.Text = "Rien de spécifique";
                                lbl_repD.Text = "Porter des bijoux";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 9: lbl_question.Text = "Quand vous manipulez les pièces d'un ordinateur, que risque de provoquer l'électricité statique ?";
                                lbl_repA.Text = "Cela peut endommager le matériel";
                                lbl_repB.Text = "Cela peut allumer l'ordinateur";
                                lbl_repC.Text = "Cela peut brûler les composants";
                                lbl_repD.Text = "Cela ne provoquera aucune anomalie";
                                bonneReponse = (int)bonneRep.A;
                                break;
                    }
                    break;

                case 1: // Troll
                    numQuestion = rnd.Next(10);
                    switch (numQuestion)
                    {
                        case 0: lbl_question.Text = "Que signifie ::1 ?";
                                lbl_repA.Text = "C'est l'adresse du localhost";
                                lbl_repB.Text = "C'est une couche du modèle OSI sous la forme d'un signe";
                                lbl_repC.Text = "C'est quelqu'un qui a loupé son smiley";
                                lbl_repD.Text = "Cela ne signifie rien";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 1: lbl_question.Text = "Combien de couche(s) porte le modèle OSI ?";
                                lbl_repA.Text = "Quatre";
                                lbl_repB.Text = "Trois";
                                lbl_repC.Text = "Sept";
                                lbl_repD.Text = "Une, car il s'agit d'un bébé";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 2: lbl_question.Text = "Que signifie FTP ?";
                                lbl_repA.Text = "Fuis, tu pues";
                                lbl_repB.Text = "File transfer protocol";
                                lbl_repC.Text = "Rien";
                                lbl_repD.Text = "File transfer present";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 3: lbl_question.Text = "Si une prise ne rentre pas dans son port, que faut-il faire ?";
                                lbl_repA.Text = "Vérifier que la prise correspond";
                                lbl_repB.Text = "Forcer pour que ça rentre";
                                lbl_repC.Text = "Demander l'aide d'une personne plus qualifiée pour le faire";
                                lbl_repD.Text = "Lire le manuel";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 4: lbl_question.Text = "Lorsque votre disque dur fait 'clic, clic, clic', qu'est-ce que cela signifie ?";
                                lbl_repA.Text = "Votre disque dur est mal fixé";
                                lbl_repB.Text = "Votre disque dur est Hors-Service";
                                lbl_repC.Text = "Votre disque dur est en très bon état";
                                lbl_repD.Text = "Votre disque dur a de la poussière à l'intérieur";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 5: lbl_question.Text = "Que faut-il faire quand votre disque dur qui est en bon état a de la poussière à l'intérieur ?";
                                lbl_repA.Text = "Souffler dans le disque dur pour l'enlever";
                                lbl_repB.Text = "Démonter le disque dur";
                                lbl_repC.Text = "Il est impossible que la poussière rentre dans le disque dur";
                                lbl_repD.Text = "Vous jouez au lancer de disque dur";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 6: lbl_question.Text = "A quelle classe d'adresses correspond l'adresse IP suivante : 192.256.30.26 ?";
                                lbl_repA.Text = "La classe A";
                                lbl_repB.Text = "La classe B";
                                lbl_repC.Text = "La classe C";
                                lbl_repD.Text = "Aucune";
                                bonneReponse = (int)bonneRep.D;
                            break;

                        case 7: lbl_question.Text = "A quelle forme le plateau fait-il allusion? ";
                                lbl_repA.Text = "C'est une bonne question...";
                                lbl_repB.Text = "Un serpent arc-en-ciel ???";
                                lbl_repC.Text = "A un rubik's cube!";
                                lbl_repD.Text = "Au logo CS2I ?";
                                bonneReponse = (int)bonneRep.D;
                            break;

                        case 8: lbl_question.Text = "Trouvez la fin de cette phrase: 'Dans le monde, il y a 10 types de personnes...' ";
                                lbl_repA.Text = "Pourtant je suis le seul dans un type, car je suis génial";
                                lbl_repB.Text = "Ceux qui savent compter en binaire et les autres";
                                lbl_repC.Text = "Pourquoi il y a 10 types de personnes ?";
                                lbl_repD.Text = "Il n'y a pas de suite à cette phrase";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 9: lbl_question.Text = "Votre carte mère a un problème de compatibilité, que faites-vous ?";
                                lbl_repA.Text = "Vous appellez la carte père";
                                lbl_repB.Text = "Vous relisez la documentation pour savoir quelle carte mère est compatible";
                                lbl_repC.Text = "Vous ouvrez votre ordinateur et vous placez votre carte mère dans l'autre sens";
                                lbl_repD.Text = "Vous jetez votre ordinateur";
                                bonneReponse = (int)bonneRep.B;
                            break;
                    }
                    break;

                case 2: // Acronymes
                    numQuestion = rnd.Next(10);
                    switch (numQuestion)
                    {
                        case 0: lbl_question.Text = "Que signifie MAN ?";
                                lbl_repA.Text = "Homme";
                                lbl_repB.Text = "Medium Area Network";
                                lbl_repC.Text = "Metropolitan Area Network";
                                lbl_repD.Text = "Medium Angle Nanotechnology";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 1: lbl_question.Text = "Quel est l'acronyme qui donne la norme Ethernet ?";
                                lbl_repA.Text = "IEEE 802.5";
                                lbl_repB.Text = "IEEE 1284";
                                lbl_repC.Text = "IEEE 1394";
                                lbl_repD.Text = "IEEE 802.3";
                                bonneReponse = (int)bonneRep.D;
                            break;

                        case 2: lbl_question.Text = "Quelle est la norme Wifi qui ne fonctionne qu'en 5 GHz ?";
                                lbl_repA.Text = "IEEE 802.11a";
                                lbl_repB.Text = "IEEE 802.11b";
                                lbl_repC.Text = "IEEE 802.11g";
                                lbl_repD.Text = "IEEE 802.11n";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 3: lbl_question.Text = "Qu'est-ce que POST ?";
                                lbl_repA.Text = "Une société de courrier";
                                lbl_repB.Text = "Post Office Services Tasks";
                                lbl_repC.Text = "Power On Self Test";
                                lbl_repD.Text = "Public Output Self Test";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 4: lbl_question.Text = "Qu'est-ce que l'acronyme POP ?";
                                lbl_repA.Text = "Un style de musique";
                                lbl_repB.Text = "Post Office Protocol";
                                lbl_repC.Text = "Power On Post";
                                lbl_repD.Text = "La réponse D";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 5: lbl_question.Text = "Quel autre acronyme a la même fonction que POP ?";
                                lbl_repA.Text = "IMAP";
                                lbl_repB.Text = "CORN";
                                lbl_repC.Text = "SMTP";
                                lbl_repD.Text = "NTFS";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 6: lbl_question.Text = "Quel est l'acronyme  désignant un réseau local ?";
                                lbl_repA.Text = "« L'âne »";
                                lbl_repB.Text = "VPN";
                                lbl_repC.Text = "LAN";
                                lbl_repD.Text = "WAN";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 7: lbl_question.Text = "Que représente l'acronyme GUI ?";
                                lbl_repA.Text = "L'interface graphique de l'utilisateur";
                                lbl_repB.Text = "Le nom d'un organisme de normalisation";
                                lbl_repC.Text = "L'unité générale de l'Intranet ?";
                                lbl_repD.Text = "Une sous-arbrisseau";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 8: lbl_question.Text = "Quel organisme propose régulièrement des normes pour Internet ?";
                                lbl_repA.Text = "Free";
                                lbl_repB.Text = "Alice ADSL";
                                lbl_repC.Text = "RAID";
                                lbl_repD.Text = "IETF";
                                bonneReponse = (int)bonneRep.D;
                            break;

                        case 9: lbl_question.Text = "Lequel de ces acronymes n'est pas un langage ou un script ?";
                                lbl_repA.Text = "PS";
                                lbl_repB.Text = "PCL";
                                lbl_repC.Text = "PnP";
                                lbl_repD.Text = "PDL";
                                bonneReponse = (int)bonneRep.C;
                            break;
                    }
                    break;

                case 3: // Commandes
                    numQuestion = rnd.Next(10);
                    switch (numQuestion)
                    {
                        case 0: lbl_question.Text = "Quelle commande permet de modifier un répertoire ?";
                                lbl_repA.Text = "dir";
                                lbl_repB.Text = "cd";
                                lbl_repC.Text = "chkdsk";
                                lbl_repD.Text = "exit";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 1: lbl_question.Text = "Quelle commande permet d'afficher le contenu d'un répertoire ?";
                                lbl_repA.Text = "dir";
                                lbl_repB.Text = "cd";
                                lbl_repC.Text = "chkdsk";
                                lbl_repD.Text = "exit";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 2: lbl_question.Text = "Quel nom possède la commande de diagnostic réseau la plus utilisée ?";
                                lbl_repA.Text = "pins";
                                lbl_repB.Text = "ntbackup";
                                lbl_repC.Text = "ping";
                                lbl_repD.Text = "arp-a";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 3: lbl_question.Text = "Quel mot accompagne  la phrase « Invite de commande » ?";
                                lbl_repA.Text = "Windows";
                                lbl_repB.Text = "Visual Studio";
                                lbl_repC.Text = "DOS";
                                lbl_repD.Text = "Avast";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 4: lbl_question.Text = "Quelle commande faut-il éviter d'actionner sauf urgence ?";
                                lbl_repA.Text = "exit";
                                lbl_repB.Text = "start";
                                lbl_repC.Text = "shutdown";
                                lbl_repD.Text = "Format C:";
                                bonneReponse = (int)bonneRep.D;
                            break;

                        case 5: lbl_question.Text = "Comment faire pour déplacer un fichier sous l'interface graphique de Windows ?";
                                lbl_repA.Text = "Je démonte le disque et déplace le fichier avec une pince à épiler !";
                                lbl_repB.Text = "J'appelle  un télékinésiste pour déplacer les fichiers par la pensée !";
                                lbl_repC.Text = "J'utilise la commande MOVE.";
                                lbl_repD.Text = "J'utilise FORMAT.";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 6: lbl_question.Text = "Quelle est une des utilités de la commande « cd » ?";
                                lbl_repA.Text = "Lire un CD dans le lecteur optique";
                                lbl_repB.Text = "À rien";
                                lbl_repC.Text = "À changer de répertoire";
                                lbl_repD.Text = "À créer un fichier sur un CD";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 7: lbl_question.Text = "Quelle extension de fichier permet d'exécuter un programme en ligne de commande DOS directement ?";
                                lbl_repA.Text = ".jpg";
                                lbl_repB.Text = ".bat";
                                lbl_repC.Text = ".exe";
                                lbl_repD.Text = ".avi";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 8: lbl_question.Text = "Que va m'afficher la commande « cmd/? » ?";
                                lbl_repA.Text = "Rien";
                                lbl_repB.Text = "L'aide sur la console";
                                lbl_repC.Text = "Une erreur car /? n'est pas reconnu";
                                lbl_repD.Text = "L'aide sur la ligne de commande en question";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 9: lbl_question.Text = "Il y a trop d'informations à l'écran, que faire pour effacer l'affichage actuel ?";
                                lbl_repA.Text = "cls";
                                lbl_repB.Text = "help";
                                lbl_repC.Text = "exit";
                                lbl_repD.Text = "shutdown";
                                bonneReponse = (int)bonneRep.A;
                            break;
                    }
                    break;
                    
                case 4: // Réseau
                    numQuestion = rnd.Next(10);
                    switch (numQuestion)
                    {
                        case 0: lbl_question.Text = "À quoi correspond l'adresse 255.255.0.0 ?";
                                lbl_repA.Text = "Au masque de sous-réseau par défaut des adresses IP de la classe A";
                                lbl_repB.Text = "À une adresse de réseau de la classe A";
                                lbl_repC.Text = "Au masque de sous-réseau par défaut des adresses IP de la classe B";
                                lbl_repD.Text = "À une adresse de réseau de la classe C";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 1: lbl_question.Text = "Quel protocole propose des services pour le transfert  et le traitement des fichiers ?";
                                lbl_repA.Text = "SSH";
                                lbl_repB.Text = "FTP";
                                lbl_repC.Text = "IMAP";
                                lbl_repD.Text = "Telnet";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 2: lbl_question.Text = "Quelle topologie physique de réseau local est dotée d'un point de connexion central auquel est relié individuellement chaque hôte du réseau par un segment câblé ?";
                                lbl_repA.Text = "Topologie en étoile";
                                lbl_repB.Text = "Topologie maillée";
                                lbl_repC.Text = "Topologie hiérarchique ou en étoile étendue";
                                lbl_repD.Text = "Topologie en anneau";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 3: lbl_question.Text = "À quelle(s) couche(s) du modèle OSI correspond la couche application du modèle TCP/IP ?";
                                lbl_repA.Text = "Application, Transport et Réseau";
                                lbl_repB.Text = "Application";
                                lbl_repC.Text = "Application, Présentation et Session";
                                lbl_repD.Text = "Liaison de données";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 4: lbl_question.Text = "Quel est le PDU (Protocol Data Unit) de la couche 3 du modèle OSI ?";
                                lbl_repA.Text = "Paquet";
                                lbl_repB.Text = "Trame";
                                lbl_repC.Text = "Signal";
                                lbl_repD.Text = "Segment";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 5: lbl_question.Text = "Quelle est la famille des dispositifs d'interconnexion de la couche 1 du modèle OSI ?";
                                lbl_repA.Text = "Les connecteurs";
                                lbl_repB.Text = "Les routeurs";
                                lbl_repC.Text = "Les ponts";
                                lbl_repD.Text = "Les répéteurs";
                                bonneReponse = (int)bonneRep.D;
                            break;

                        case 6: lbl_question.Text = "Quelle est l'adresse de réseau de l'adresse IP 88.108.55.66 ?";
                                lbl_repA.Text = "88.255.255.255";
                                lbl_repB.Text = "88.0.0.0";
                                lbl_repC.Text = "255.0.0.0";
                                lbl_repD.Text = "88.108.55.0";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 7: lbl_question.Text = "Quelle est l'adresse de diffusion restreinte de l'adresse IP : 214.36.90.240 /16 ?";
                                lbl_repA.Text = "214.36.90.255";
                                lbl_repB.Text = "255.255.255.0";
                                lbl_repC.Text = "214.36.255.255";
                                lbl_repD.Text = "255.255.0.0";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 8: lbl_question.Text = "Qu'est-ce qu'un réseau étendu ?";
                                lbl_repA.Text = "Un réseau qui s'étend à perte de vue";
                                lbl_repB.Text = "Un réseau regroupant des périphériques interconnectés et placés sous un contrôle administrateur unique";
                                lbl_repC.Text = "Un réseau qui connecte plusieurs réseaux locaux situés dans des zones géographiques distinctes";
                                lbl_repD.Text = "Un réseau local auquel on ajoute de nouveaux périphériques";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 9: lbl_question.Text = "Qu'est-ce qu'un réseau peer to peer ?";
                                lbl_repA.Text = "Un réseau où le client demande des informations ou des services au serveur";
                                lbl_repB.Text = "Un réseau où il n'y a ni serveur dédié ni hiérarchie entre les ordinateurs";
                                lbl_repC.Text = "Un réseau qu'il faut regarder attentivement";
                                lbl_repD.Text = "Un réseau qui va de plus en plus mal";
                                bonneReponse = (int)bonneRep.B;
                            break;
                    }
                    break;

                case 5: // Dépannage
                    numQuestion = rnd.Next(10);
                    switch (numQuestion)
                    {
                        case 0: lbl_question.Text = "Quelle précaution faut-il prendre avant de commencer le dépannage d'un ordinateur ?";
                                lbl_repA.Text = "Vérifier qu'on sera payé";
                                lbl_repB.Text = "Effectuer une sauvegarde des données";
                                lbl_repC.Text = "Vérifier que l'ordinateur est toujours sous garantie";
                                lbl_repD.Text = "Contrôler le boîtier de l'ordinateur";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 1: lbl_question.Text = "Quel outil Windows permet d'avoir des informations sur les erreurs rencontrées par le système ?";
                                lbl_repA.Text = "Le gestionnaire d'identifications";
                                lbl_repB.Text = "Le gestionnaire des tâches";
                                lbl_repC.Text = "Le gestionnaire de périphériques";
                                lbl_repD.Text = "L'observateur d'évènements";
                                bonneReponse = (int)bonneRep.D;
                            break;

                        case 2: lbl_question.Text = "Quelle est la dernière étape à effectuer lors d'un dépannage ?";
                                lbl_repA.Text = "Vérifier la solution avec le client";
                                lbl_repB.Text = "Mettre en oeuvre la solution";
                                lbl_repC.Text = "Vérifier la solution et le fonctionnement du système";
                                lbl_repD.Text = "Déterminer la cause exacte du problème";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 3: lbl_question.Text = "Quel type de sauvegarde du disque dur sauvegarde tous les fichiers sélectionnés du disque ? ";
                                lbl_repA.Text = "La sauvegarde par copie";
                                lbl_repB.Text = "La sauvegarde complète";
                                lbl_repC.Text = "La sauvegarde incrémentielle";
                                lbl_repD.Text = "La sauvegarde différentielle";
                                bonneReponse = (int)bonneRep.B;
                            break;

                        case 4: lbl_question.Text = "Quelle peut être la cause d'un ordinateur qui se bloque ?";
                                lbl_repA.Text = "L'ordinateur fait une pause";
                                lbl_repB.Text = "L'ordinateur est exposé à une température de 20°C";
                                lbl_repC.Text = "L'utilisateur a appuyé trop rapidement sur les touches";
                                lbl_repD.Text = "L'ordinateur est en surchauffe";
                                bonneReponse = (int)bonneRep.D;
                            break;

                        case 5: lbl_question.Text = "Quelle solution pouvez-vous essayer d'apporter si un ordinateur portable ne s'allume pas ?";
                                lbl_repA.Text = "Remplacer la batterie si elle ne se recharge plus";
                                lbl_repB.Text = "Appuyer sur une touche du clavier pour que l'ordinateur sorte du mode veille";
                                lbl_repC.Text = "Régler l'écran LCD sur la résolution native";
                                lbl_repD.Text = "Générer suffisamment d'énergie pour qu'il s'allume en appuyant rapidement sur les touches";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 6: lbl_question.Text = "Que faire si un document d'application ne s'imprime pas ?";
                                lbl_repA.Text = "Redémarrer l'ordinateur";
                                lbl_repB.Text = "Prier l'imprimante de bien vouloir imprimer ce document";
                                lbl_repC.Text = "Débrancher et rebrancher l'imprimante";
                                lbl_repD.Text = "Supprimer le document de la file d'attente et recommencer l'impression";
                                bonneReponse = (int)bonneRep.D;
                            break;

                        case 7: lbl_question.Text = "Les voyants de la carte réseau ne s'allument pas, quelle peut en être la cause ?";
                                lbl_repA.Text = "Le pare-feu bloque le port 23";
                                lbl_repB.Text = "Le routeur est hors tension ou la connexion est de mauvaise qualité";
                                lbl_repC.Text = "Le câble réseau est débranché, défectueux ou endommagé";
                                lbl_repD.Text = "L'adresse de passerelle est incorrecte";
                                bonneReponse = (int)bonneRep.C;
                            break;

                        case 8: lbl_question.Text = "Quelle peut être la cause de la réception de centaines ou de milliers de courriers électroniques non désirés par un ordinateur ?";
                                lbl_repA.Text = "Aucune détection ou protection du serveur de messagerie contre les spams n'est assuré par le réseau";
                                lbl_repB.Text = "L'utilisateur a beaucoup d'amis";
                                lbl_repC.Text = "L'utilisateur est une personne très demandée";
                                lbl_repD.Text = "L'utilisateur adore la lecture";
                                bonneReponse = (int)bonneRep.A;
                            break;

                        case 9: lbl_question.Text = "Quelle peut être la cause de la non exécution d'Aero par un ordinateur équipé de Windows Vista ?";
                                lbl_repA.Text = "Un processus en cours utilise la plupart des ressources du processeur";
                                lbl_repB.Text = "La configuration matérielle minimale requise pour exécuter Aero n'est pas respectée par l'ordinateur";
                                lbl_repC.Text = "Windows Vista est à bout de souffle";
                                lbl_repD.Text = "L'ordinateur est en surchauffe";
                                bonneReponse = (int)bonneRep.B;
                            break;
                    }
                    break;
            }

            if (((numJoueur == 1 && brepj1 >= 2) || (numJoueur == 2 && brepj2 >= 2)) && modeBug == true)
            {
                switch (repdisparue)
                {
                    case 0: lbl_repA.Text = "?"; break;
                    case 1: lbl_repB.Text = "?"; break;
                    case 2: lbl_repC.Text = "?"; break;
                    case 3: lbl_repD.Text = "?"; break;
                }
            }

            if ((numQuestionBug == 3 || numQuestionBug == 7) && modeBug == true)
            {
                //on choisit un bug:
                switch (rnd.Next(2))
                {
                    case 0: bugQuestion1(lbl_question); break;

                    case 1: 
                        reponsesBug = true;
                        btn_repA.BringToFront();
                        btn_repB.BringToFront();
                        btn_repC.BringToFront();
                        btn_repD.BringToFront();
                        break;
                }
            }
        }

        //choix de la question initiale:
        public void init()
        {
            btn1select = false;
            btn2select = false;
            btn3select = false;
            btn4select = false;
            erepA = 0;
            erepB = 0;
            erepC = 0;
            erepD = 0;
            btn_repA.BackColor = Color.FromArgb(224, 224, 224);
            btn_repB.BackColor = Color.FromArgb(224, 224, 224);
            btn_repC.BackColor = Color.FromArgb(224, 224, 224);
            btn_repD.BackColor = Color.FromArgb(224, 224, 224);

            int tirageausort = rnd.Next(4);

            switch (tirageausort)
            {
                case 0: lbl_question.Text = "Mettez dans l'ordre ces étapes du processus d'impression d'une imprimante laser.";
                    lbl_repA.Text = "Chauffage";
                    lbl_repB.Text = "Développement";
                    lbl_repC.Text = "Transfert";
                    lbl_repD.Text = "Écriture";
                    brepD = 1;
                    brepB = 2;
                    brepC = 3;
                    brepA = 4;
                    break;
                case 1: lbl_question.Text = "Mettez dans l'ordre ces couches du modèle OSI (de la plus basse à la plus haute).";
                    lbl_repA.Text = "Physique";
                    lbl_repB.Text = "Application";
                    lbl_repC.Text = "Réseau";
                    lbl_repD.Text = "Transport";
                    brepA = 1;
                    brepC = 2;
                    brepD = 3;
                    brepB = 4;
                    break;
                case 2: lbl_question.Text = "Mettez dans l'ordre ces étapes de mise à jour d'un pilote d'imprimante.";
                    lbl_repA.Text = "Tester le nouveau pilote de l'imprimante";
                    lbl_repB.Text = "Télécharger le pilote";
                    lbl_repC.Text = "Rechercher si un nouveau pilote est disponible";
                    lbl_repD.Text = "Installer le pilote téléchargé automatiquement ou manuellement";
                    brepC = 1;
                    brepB = 2;
                    brepD = 3;
                    brepA = 4;
                    break;
                case 3: lbl_question.Text = "Quel est l'ordre requis dans les messages DHCP suivants pour qu'un hôte puisse obtenir une adresse IP auprès d'un serveur DHCP ?";
                    lbl_repA.Text = "Offre DHCP";
                    lbl_repB.Text = "Accusé de réception DHCP";
                    lbl_repC.Text = "Découverte DHCP";
                    lbl_repD.Text = "Requête DHCP";
                    brepC = 1;
                    brepA = 2;
                    brepD = 3;
                    brepB = 4;
                    break;
            }
        }

        //une fois le premier joueur pret, on initialise le second:
        public void initSecondJoueur(int numLab)
        {
            bool initSecondJoueur = false;
            //on change le numero du personnage:
            if (numJoueur == 1 && initSecondJoueur == false)
            {
                p1.numLabEnCours = numLab;
                p1.lblEnCours = cases[p1.numLabEnCours];
                lbl_player1.Location = new Point(cases[p1.numLabEnCours].Location.X + 2, cases[p1.numLabEnCours].Location.Y + cases[p1.numLabEnCours].Height / 10);
                lbl_player1.BackColor = cases[p1.numLabEnCours].BackColor;
                departJ1 = false;
                initSecondJoueur = true;
                numJoueur = 2;
                lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();
                btn1select = false;
                btn2select = false;
                btn3select = false;
                btn4select = false;
                if (departJ2 == true)
                {
                    init();
                }
                else
                {
                    recupererCategorie(p2.lblEnCours.BackColor);
                    genererQuestion(categQuestion);
                }
            }
            else if (numJoueur == 2 && initSecondJoueur == false)
            {
                p2.numLabEnCours = numLab;
                p2.lblEnCours = cases[p2.numLabEnCours];
                lbl_player2.Location = new Point(cases[p2.numLabEnCours].Location.X + 2, cases[p2.numLabEnCours].Location.Y + cases[p2.numLabEnCours].Height / 10);
                lbl_player2.BackColor = cases[p2.numLabEnCours].BackColor;
                departJ2 = false;
                initSecondJoueur = true;
                numJoueur = 1;
                lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();
                btn1select = false;
                btn2select = false;
                btn3select = false;
                btn4select = false;
                if (departJ1 == true)
                {
                    init();
                }
                else
                {
                    recupererCategorie(p1.lblEnCours.BackColor);
                    genererQuestion(categQuestion);
                }
            }

            if (departJ1 == false && departJ2 == false && jeuChronometre==true)
            {
                lblChrono.Visible = true;
            }
        }

        //vérification que les réponses données par l'utilisateur sont celles définies à l'initialisation:
        public void verif_init()
        {
            if (btn1select == true && btn2select == true && btn3select == true && btn4select == true)
            {
                if (brepA == erepA && brepB == erepB && brepC == erepC && brepD == erepD)
                {
                    btn_repA.Text = "A";
                    btn_repB.Text = "B";
                    btn_repC.Text = "C";
                    btn_repD.Text = "D";
                    initSecondJoueur(4);
                }
                else if (brepA != erepA && brepB != erepB && brepC != erepC && brepD != erepD)
                {
                    btn_repA.Text = "A";
                    btn_repB.Text = "B";
                    btn_repC.Text = "C";
                    btn_repD.Text = "D";
                    initSecondJoueur(1);
                }
                else if ((brepA == erepA && brepB != erepB && brepC != erepC && brepD != erepD)
                    || (brepA != erepA && brepB == erepB && brepC != erepC && brepD != erepD)
                    || (brepA != erepA && brepB != erepB && brepC == erepC && brepD != erepD)
                    || (brepA != erepA && brepB != erepB && brepC != erepC && brepD == erepD))
                {
                    btn_repA.Text = "A";
                    btn_repB.Text = "B";
                    btn_repC.Text = "C";
                    btn_repD.Text = "D";
                    initSecondJoueur(2);
                }
                else if ((brepA == erepA && brepB == erepB && brepC != erepC && brepD != erepD)
                    || (brepA == erepA && brepB != erepB && brepC == erepC && brepD != erepD)
                    || (brepA == erepA && brepB != erepB && brepC != erepC && brepD == erepD)
                    || (brepA != erepA && brepB == erepB && brepC == erepC && brepD != erepD)
                    || (brepA != erepA && brepB == erepB && brepC != erepC && brepD == erepD)
                    || (brepA != erepA && brepB != erepB && brepC == erepC && brepD == erepD))
                {
                    btn_repA.Text = "A";
                    btn_repB.Text = "B";
                    btn_repC.Text = "C";
                    btn_repD.Text = "D";
                    initSecondJoueur(3);
                }
                btn_repA.BackColor = Color.FromArgb(224, 224, 224);
                btn_repB.BackColor = Color.FromArgb(224, 224, 224);
                btn_repC.BackColor = Color.FromArgb(224, 224, 224);
                btn_repD.BackColor = Color.FromArgb(224, 224, 224);
            }
        }
        
        //Si c'est la bonne réponse:
        public void repCorrecte()
        {
            pass_tr = 0;
            //si la réponse est bonne, alors:
            //quel joueur deplace t-on:
            if (numJoueur == 1)
            {
                brepj1++;
                p1.deplPerso(true);

                //on affiche les nouvelles positions:
                lbl_player1.Location = new Point(cases[p1.numLabEnCours].Location.X + 2, cases[p1.numLabEnCours].Location.Y + cases[p1.numLabEnCours].Height / 10);
                lbl_player1.BackColor = cases[p1.numLabEnCours].BackColor;
                p1.lblEnCours = cases[p1.numLabEnCours];

                case_retour();

                if (p1.numLabEnCours == 63)
                {
                    DialogResult res;
                    res = MessageBox.Show("Le joueur 1 a gagné ! Rejouer ? ", "Fin de partie", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                        reinit();
                    else
                        this.Close();
                }
                else
                {
                    //on change de joueur!
                    numJoueur = 2;
                    if (modeBug == false || (modeBug == true && (p2.numLabEnCours != case_pass_tr1 || p2.numLabEnCours != case_pass_tr2 || p2.numLabEnCours != case_pass_tr3)))
                    {
                        lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();

                        //on recharge une question pour le nouveau joueur:
                        recupererCategorie(p2.lblEnCours.BackColor);
                        genererQuestion(categQuestion);
                    }
                    if (modeBug == true)
                    {
                        if (pass_tr >= 0 && pass_tr < 2)
                        {
                            passage_tour();
                            lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();
                            if (pass_tr == 1)
                            {
                                passage_tour();
                                lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();
                                recupererCategorie(p1.lblEnCours.BackColor);
                                genererQuestion(categQuestion);
                            }
                            else
                            {
                                recupererCategorie(p2.lblEnCours.BackColor);
                                genererQuestion(categQuestion);
                            }
                        }
                    }
                }
            }
            else
            {
                brepj2++;
                p2.deplPerso(true);

                //on affiche les nouvelles positions:
                lbl_player2.Location = new Point(cases[p2.numLabEnCours].Location.X + 2, cases[p2.numLabEnCours].Location.Y + cases[p2.numLabEnCours].Height / 10);
                lbl_player2.BackColor = cases[p2.numLabEnCours].BackColor;
                p2.lblEnCours = cases[p2.numLabEnCours];

                case_retour();

                if (p2.numLabEnCours == 63)
                {
                    DialogResult res;
                    res = MessageBox.Show("Le joueur 2 a gagné ! Rejouer? ", "Fin de partie", MessageBoxButtons.YesNo);
                    if (res == DialogResult.Yes)
                        reinit();
                    else
                        this.Close();
                }
                else
                {
                    numJoueur = 1;

                    if (modeBug == false || (modeBug == true && (p1.numLabEnCours != case_pass_tr1 || p1.numLabEnCours != case_pass_tr2 || p1.numLabEnCours != case_pass_tr3)))
                    {
                        lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();

                        //on recharge une question pour le nouveau joueur:
                        recupererCategorie(p1.lblEnCours.BackColor);
                        genererQuestion(categQuestion);
                    }
                    if (modeBug == true)
                    {
                        if (pass_tr >= 0 && pass_tr < 2)
                        {
                            passage_tour();
                            lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();
                            if (pass_tr == 1)
                            {
                                passage_tour();
                                lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();
                                recupererCategorie(p2.lblEnCours.BackColor);
                                genererQuestion(categQuestion);
                            }
                            else
                            {
                                recupererCategorie(p1.lblEnCours.BackColor);
                                genererQuestion(categQuestion);
                            }
                        }
                    }
                }
            }
        }

        //si c'est la mauvaise réponse:
        public void repFausse()
        {
            pass_tr = 0;
            //sinon:
            //quel joueur recule t-on:
            if (numJoueur == 1)
            {
                brepj1=0;
                p1.deplPerso(false);

                //on affiche les nouvelles positions:
                lbl_player1.Location = new Point(cases[p1.numLabEnCours].Location.X + 2, cases[p1.numLabEnCours].Location.Y + cases[p1.numLabEnCours].Height / 10);
                lbl_player1.BackColor = cases[p1.numLabEnCours].BackColor;
                p1.lblEnCours = cases[p1.numLabEnCours];

                case_retour();


                numJoueur = 2;
                if (modeBug == false || (modeBug == true && (p2.numLabEnCours != case_pass_tr1 || p2.numLabEnCours != case_pass_tr2 || p2.numLabEnCours != case_pass_tr3)))
                {
                    lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();

                    //on recharge une question pour le nouveau joueur:
                    recupererCategorie(p2.lblEnCours.BackColor);
                    genererQuestion(categQuestion);
                }
                if (modeBug == true)
                {
                    if (pass_tr >= 0 && pass_tr < 2)
                    {
                        passage_tour();
                        lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();
                        if (pass_tr == 1)
                        {
                            passage_tour();
                            lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();
                            recupererCategorie(p1.lblEnCours.BackColor);
                            genererQuestion(categQuestion);
                        }
                        else
                        {
                            recupererCategorie(p2.lblEnCours.BackColor);
                            genererQuestion(categQuestion);
                        }
                    }
                }
                
            }
            else
            {
                brepj2=0;
                p2.deplPerso(false);

                //on affiche les nouvelles positions:
                lbl_player2.Location = new Point(cases[p2.numLabEnCours].Location.X + 2, cases[p2.numLabEnCours].Location.Y + cases[p2.numLabEnCours].Height / 10);
                lbl_player2.BackColor = cases[p2.numLabEnCours].BackColor;
                p2.lblEnCours = cases[p2.numLabEnCours];

                case_retour();

                numJoueur = 1;

                if (modeBug == false || (modeBug == true && (p1.numLabEnCours != case_pass_tr1 || p1.numLabEnCours != case_pass_tr2 || p1.numLabEnCours != case_pass_tr3)))
                {
                    lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();

                    //on recharge une question pour le nouveau joueur:
                    recupererCategorie(p1.lblEnCours.BackColor);
                    genererQuestion(categQuestion);
                }
                if (modeBug == true)
                {
                    if (pass_tr >= 0 && pass_tr < 2)
                    {
                        passage_tour();
                        lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();
                        if (pass_tr == 1)
                        {
                            passage_tour();
                            lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();
                            recupererCategorie(p2.lblEnCours.BackColor);
                            genererQuestion(categQuestion);
                        }
                        else
                        {
                            recupererCategorie(p1.lblEnCours.BackColor);
                            genererQuestion(categQuestion);
                        }
                    }
                }
            }
        }

        //reinitilisation complete:
        public void reinit()
        {
            departJ1 = true;
            departJ2 = true;

            p1 = new personnage(cases[0], lbl_player1);
            p2 = new personnage(cases[0], lbl_player2);

            //On choisira une ou plusieurs questions de base pour le choix du Joueur qui jouera en premier!
            question = "";
            repA = "";
            repB = "";
            repC = "";
            repD = "";

            lbl_question.Text = question;
            lbl_repA.Text = repA;
            lbl_repB.Text = repB;
            lbl_repC.Text = repC;
            lbl_repD.Text = repD;

            //si il y'a le bug 2 en cours, on le stop:
            if (reponsesBug == true)
            {
                reponsesBug = false;
                replaceBtnRep();
            }

            // initialisation des cases de passage de tour
            case_pass_tr1 = 11 + rnd.Next(16);
            case_pass_tr2 = 27 + rnd.Next(16);
            case_pass_tr3 = 43 + rnd.Next(14);
            pass_tr_J1 = false;
            pass_tr_J2 = false;
            pass_tr = 0;

            // initialisation des cases de retour à la première case du plateau
            do
            {
                case_ret_dep_1J = 11 + rnd.Next(46);
            } while (case_ret_dep_1J == case_pass_tr1 || case_ret_dep_1J == case_pass_tr2 || case_ret_dep_1J == case_pass_tr3);

            do
            {
                case_ret_dep_2J = 11 + rnd.Next(46);
            } while (case_ret_dep_1J == case_ret_dep_2J || case_ret_dep_2J == case_pass_tr1 || case_ret_dep_2J == case_pass_tr2 || case_ret_dep_2J == case_pass_tr3);


            //pour le moment: joueur par defaut = joueur 1, position = case[1]:
            //initialisation du joueur qui commence:
            numJoueur = rnd.Next(2) + 1;

            //on commence avec le J1:
            p1.numLabEnCours = 0;
            lbl_player1.Location = new Point(cases[p1.numLabEnCours].Location.X + cases[p1.numLabEnCours].Width / 6, cases[p1.numLabEnCours].Location.Y + cases[p1.numLabEnCours].Height / 4);
            lbl_player1.BackColor = cases[p1.numLabEnCours].BackColor;
            p1.lblEnCours = cases[p1.numLabEnCours];
            if (departJ1 == true)
            {
                init();
                lblChrono.Visible = false;
            }

            //on commence avec le J2:
            p2.numLabEnCours = 0;
            lbl_player2.Location = new Point(cases[p2.numLabEnCours].Location.X + cases[p2.numLabEnCours].Width / 6, cases[p2.numLabEnCours].Location.Y + cases[p2.numLabEnCours].Height / 4);
            lbl_player2.BackColor = cases[p2.numLabEnCours].BackColor;
            p2.lblEnCours = cases[p2.numLabEnCours];
            if (departJ2 == true)
            {
                init();
                lblChrono.Visible = false;
            }
        }

        //bug de passage de tour:
        public void passage_tour()
        {
            if(numJoueur==1)
            {
                if ((p1.numLabEnCours == case_pass_tr1 || p1.numLabEnCours == case_pass_tr2 || p1.numLabEnCours == case_pass_tr3) && pass_tr_J1 == false)
                {
                    timer1.Stop();
                    MessageBox.Show("Error 404 : question not found ! \r\n    Vous passez votre tour!");
                    timer1.Start();
                    numJoueur = 2;
                    pass_tr_J1 = true;
                    pass_tr++;
                }
                else if (p1.numLabEnCours != case_pass_tr1 || p1.numLabEnCours != case_pass_tr2 || p1.numLabEnCours != case_pass_tr3)
                {
                        pass_tr_J1 = false;
                }
            }
            else
            {
                if ((p2.numLabEnCours == case_pass_tr1 || p2.numLabEnCours == case_pass_tr2 || p2.numLabEnCours == case_pass_tr3) && pass_tr_J2 == false)
                {
                    timer1.Stop();
                    MessageBox.Show("Error 404 : question not found ! \r\n   Vous passez votre tour!");
                    timer1.Start();
                    numJoueur = 1;
                    pass_tr_J2 = true;
                    pass_tr++;
                }
                else if (p2.numLabEnCours != case_pass_tr1 || p2.numLabEnCours != case_pass_tr2 || p2.numLabEnCours != case_pass_tr3)
                {
                    pass_tr_J2 = false;
                }
            }
        }

        //bug de retour à la case départ:
        public void case_retour()
        {
            if (modeBug == true)
            {
                if (p1.numLabEnCours == case_ret_dep_1J)
                {
                    timer1.Stop();
                    MessageBox.Show("Le disque dur du Joueur 1 a été formaté !");
                    timer1.Start();
                    p1.numLabEnCours = 1;
                    brepj1 = 0;
                    lbl_player1.Location = new Point(cases[p1.numLabEnCours].Location.X + 2, cases[p1.numLabEnCours].Location.Y + cases[p1.numLabEnCours].Height / 10);
                    lbl_player1.BackColor = cases[p1.numLabEnCours].BackColor;
                    p1.lblEnCours = cases[p1.numLabEnCours];
                }

                if (p2.numLabEnCours == case_ret_dep_1J)
                {
                    timer1.Stop();
                    MessageBox.Show("Le disque dur du Joueur 2 a été formaté !");
                    timer1.Start();
                    p2.numLabEnCours = 1;
                    brepj2 = 0;
                    lbl_player2.Location = new Point(cases[p2.numLabEnCours].Location.X + 2, cases[p2.numLabEnCours].Location.Y + cases[p2.numLabEnCours].Height / 10);
                    lbl_player2.BackColor = cases[p2.numLabEnCours].BackColor;
                    p2.lblEnCours = cases[p2.numLabEnCours];
                }

                if (p2.numLabEnCours == case_ret_dep_2J || p1.numLabEnCours== case_ret_dep_2J)
                {
                    timer1.Stop();
                    MessageBox.Show("Les disques durs des deux joueurs ont été formatés !");
                    timer1.Start();
                    p1.numLabEnCours = 1;
                    p2.numLabEnCours = 1;
                    brepj1 = 0;
                    brepj2 = 0;
                    lbl_player1.Location = new Point(cases[p1.numLabEnCours].Location.X + 2, cases[p1.numLabEnCours].Location.Y + cases[p1.numLabEnCours].Height / 10);
                    lbl_player1.BackColor = cases[p1.numLabEnCours].BackColor;
                    p1.lblEnCours = cases[p1.numLabEnCours];
                    lbl_player2.Location = new Point(cases[p2.numLabEnCours].Location.X + 2, cases[p2.numLabEnCours].Location.Y + cases[p2.numLabEnCours].Height / 10);
                    lbl_player2.BackColor = cases[p2.numLabEnCours].BackColor;
                    p2.lblEnCours = cases[p2.numLabEnCours];
                }
            }
        }

        /************************************************************************************************/

        //methodes evenementielles:
        private void btn_repA_Click(object sender, EventArgs e)
        {
            //si il y'a le bug 2 en cours, on le stop:
            if (reponsesBug == true)
            {
                reponsesBug = false;
                replaceBtnRep();
            }
            //quel est le joueur concerné:
            if (numJoueur == 1 && departJ1 == true)
            {
                btn_repA.BackColor = Color.DarkGray;
                if (btn1select == false && btn2select == false && btn3select == false && btn4select == false)
                {
                    erepA = 1;
                    btn_repA.Text = Convert.ToString(erepA);
                    btn1select = true;
                }
                if ((btn1select == false && btn2select == true && btn3select == false && btn4select == false)
                    || (btn1select == false && btn2select == false && btn3select == true && btn4select == false)
                    || (btn1select == false && btn2select == false && btn3select == false && btn4select == true))
                {
                    erepA = 2;
                    btn_repA.Text = Convert.ToString(erepA);
                    btn1select = true;
                }
                if ((btn1select == false && btn2select == true && btn3select == true && btn4select == false)
                    || (btn1select == false && btn2select == false && btn3select == true && btn4select == true)
                    || (btn1select == false && btn2select == true && btn3select == false && btn4select == true))
                {
                    erepA = 3;
                    btn_repA.Text = Convert.ToString(erepA);
                    btn1select = true;
                }
                if (btn1select == false && btn2select == true && btn3select == true && btn4select == true)
                {
                    erepA = 4;
                    btn_repA.Text = Convert.ToString(erepA);
                    btn1select = true;
                }
                verif_init();
            }
            else if (numJoueur == 2 && departJ2 == true)
            {
                btn_repA.BackColor = Color.DarkGray;
                if (btn1select == false && btn2select == false && btn3select == false && btn4select == false)
                {
                    erepA = 1;
                    btn_repA.Text = Convert.ToString(erepA);
                    btn1select = true;
                }
                if ((btn1select == false && btn2select == true && btn3select == false && btn4select == false)
                    || (btn1select == false && btn2select == false && btn3select == true && btn4select == false)
                    || (btn1select == false && btn2select == false && btn3select == false && btn4select == true))
                {
                    erepA = 2;
                    btn_repA.Text = Convert.ToString(erepA);
                    btn1select = true;
                }
                if ((btn1select == false && btn2select == true && btn3select == true && btn4select == false)
                    || (btn1select == false && btn2select == false && btn3select == true && btn4select == true)
                    || (btn1select == false && btn2select == true && btn3select == false && btn4select == true))
                {
                    erepA = 3;
                    btn_repA.Text = Convert.ToString(erepA);
                    btn1select = true;
                }
                if (btn1select == false && btn2select == true && btn3select == true && btn4select == true)
                {
                    erepA = 4;
                    btn_repA.Text = Convert.ToString(erepA);
                    btn1select = true;
                }
                verif_init();
            }
            else
            {
                //controle de la bonne reponse:
                if (bonneReponse == (int)bonneRep.A)
                {
                    repCorrecte();
                }
                else
                {
                    repFausse();
                }
            }//fin du else
        }

        private void btn_repB_Click(object sender, EventArgs e)
        {
            //si il y'a le bug 2 en cours, on le stop:
            if (reponsesBug == true)
            {
                reponsesBug = false;
                replaceBtnRep();
            }
 
            if (departJ1 == true)
            {
                btn_repB.BackColor = Color.DarkGray;
                if (btn2select == false && btn1select == false && btn3select == false && btn4select == false)
                {
                    erepB = 1;
                    btn_repB.Text = Convert.ToString(erepB);
                    btn2select = true;
                }
                if ((btn2select == false && btn1select == true && btn3select == false && btn4select == false)
                    || (btn2select == false && btn1select == false && btn3select == true && btn4select == false)
                    || (btn2select == false && btn1select == false && btn3select == false && btn4select == true))
                {
                    erepB = 2;
                    btn_repB.Text = Convert.ToString(erepB);
                    btn2select = true;
                }
                if ((btn2select == false && btn1select == true && btn3select == true && btn4select == false)
                    || (btn2select == false && btn1select == false && btn3select == true && btn4select == true)
                    || (btn2select == false && btn1select == true && btn3select == false && btn4select == true))
                {
                    erepB = 3;
                    btn_repB.Text = Convert.ToString(erepB);
                    btn2select = true;
                }
                if (btn2select == false && btn1select == true && btn3select == true && btn4select == true)
                {
                    erepB = 4;
                    btn_repB.Text = Convert.ToString(erepB);
                    btn2select = true;
                }
                verif_init();
            }
            else if (departJ2 == true)
            {
                btn_repB.BackColor = Color.DarkGray;
                if (btn2select == false && btn1select == false && btn3select == false && btn4select == false)
                {
                    erepB = 1;
                    btn_repB.Text = Convert.ToString(erepB);
                    btn2select = true;
                }
                if ((btn2select == false && btn1select == true && btn3select == false && btn4select == false)
                    || (btn2select == false && btn1select == false && btn3select == true && btn4select == false)
                    || (btn2select == false && btn1select == false && btn3select == false && btn4select == true))
                {
                    erepB = 2;
                    btn_repB.Text = Convert.ToString(erepB);
                    btn2select = true;
                }
                if ((btn2select == false && btn1select == true && btn3select == true && btn4select == false)
                    || (btn2select == false && btn1select == false && btn3select == true && btn4select == true)
                    || (btn2select == false && btn1select == true && btn3select == false && btn4select == true))
                {
                    erepB = 3;
                    btn_repB.Text = Convert.ToString(erepB);
                    btn2select = true;
                }
                if (btn2select == false && btn1select == true && btn3select == true && btn4select == true)
                {
                    erepB = 4;
                    btn_repB.Text = Convert.ToString(erepB);
                    btn2select = true;
                }
                verif_init();
            }
            else
            {
                //controle de la bonne reponse:
                if (bonneReponse == (int)bonneRep.B)
                {
                    repCorrecte();
                }
                else
                {
                    repFausse();
                }
            }//fin de else
        }

        private void btn_repC_Click(object sender, EventArgs e)
        {
            //si il y'a le bug 2 en cours, on le stop:
            if (reponsesBug == true)
            {
                reponsesBug = false;
                replaceBtnRep();
            }

            if (departJ1 == true)
            {
                btn_repC.BackColor = Color.DarkGray;
                if (btn3select == false && btn1select == false && btn2select == false && btn4select == false)
                {
                    erepC = 1;
                    btn_repC.Text = Convert.ToString(erepC);
                    btn3select = true;
                }
                if ((btn3select == false && btn1select == true && btn2select == false && btn4select == false)
                    || (btn3select == false && btn1select == false && btn2select == true && btn4select == false)
                    || (btn3select == false && btn1select == false && btn2select == false && btn4select == true))
                {
                    erepC = 2;
                    btn_repC.Text = Convert.ToString(erepC);
                    btn3select = true;
                }
                if ((btn3select == false && btn1select == true && btn2select == true && btn4select == false)
                    || (btn3select == false && btn1select == false && btn2select == true && btn4select == true)
                    || (btn3select == false && btn1select == true && btn2select == false && btn4select == true))
                {
                    erepC = 3;
                    btn_repC.Text = Convert.ToString(erepC);
                    btn3select = true;
                }
                if (btn3select == false && btn1select == true && btn2select == true && btn4select == true)
                {
                    erepC = 4;
                    btn_repC.Text = Convert.ToString(erepC);
                    btn3select = true;
                }
                verif_init();
            }
            else if (departJ2 == true)
            {
                btn_repC.BackColor = Color.DarkGray;
                if (btn3select == false && btn1select == false && btn2select == false && btn4select == false)
                {
                    erepC = 1;
                    btn_repC.Text = Convert.ToString(erepC);
                    btn3select = true;
                }
                if ((btn3select == false && btn1select == true && btn2select == false && btn4select == false)
                    || (btn3select == false && btn1select == false && btn2select == true && btn4select == false)
                    || (btn3select == false && btn1select == false && btn2select == false && btn4select == true))
                {
                    erepC = 2;
                    btn_repC.Text = Convert.ToString(erepC);
                    btn3select = true;
                }
                if ((btn3select == false && btn1select == true && btn2select == true && btn4select == false)
                    || (btn3select == false && btn1select == false && btn2select == true && btn4select == true)
                    || (btn3select == false && btn1select == true && btn2select == false && btn4select == true))
                {
                    erepC = 3;
                    btn_repC.Text = Convert.ToString(erepC);
                    btn3select = true;
                }
                if (btn3select == false && btn1select == true && btn2select == true && btn4select == true)
                {
                    erepC = 4;
                    btn_repC.Text = Convert.ToString(erepC);
                    btn3select = true;
                }
                verif_init();
            }
            else
            {
                //controle de la bonne reponse:
                if (bonneReponse == (int)bonneRep.C)
                {
                    repCorrecte();
                }
                else
                {
                    repFausse();
                }
            }//fin du else
        }

        private void btn_repD_Click(object sender, EventArgs e)
        {
            //si il y'a le bug 2 en cours, on le stop:
            if (reponsesBug == true)
            {
                reponsesBug = false;
                replaceBtnRep();
            }

            if (departJ1 == true)
            {
                btn_repD.BackColor = Color.DarkGray;
                if (btn4select == false && btn1select == false && btn2select == false && btn3select == false)
                {
                    erepD = 1;
                    btn_repD.Text = Convert.ToString(erepD);
                    btn4select = true;
                }
                if ((btn4select == false && btn1select == true && btn2select == false && btn3select == false)
                    || (btn4select == false && btn1select == false && btn2select == true && btn3select == false)
                    || (btn4select == false && btn1select == false && btn2select == false && btn3select == true))
                {
                    erepD = 2;
                    btn_repD.Text = Convert.ToString(erepD);
                    btn4select = true;
                }
                if ((btn4select == false && btn1select == true && btn2select == true && btn3select == false)
                    || (btn4select == false && btn1select == false && btn2select == true && btn3select == true)
                    || (btn4select == false && btn1select == true && btn2select == false && btn3select == true))
                {
                    erepD = 3;
                    btn_repD.Text = Convert.ToString(erepD);
                    btn4select = true;
                }
                if (btn4select == false && btn1select == true && btn2select == true && btn3select == true)
                {
                    erepD = 4;
                    btn_repD.Text = Convert.ToString(erepD);
                    btn4select = true;
                }
                verif_init();
            }
            else if (departJ2 == true)
            {
                btn_repD.BackColor = Color.DarkGray;
                if (btn4select == false && btn1select == false && btn2select == false && btn3select == false)
                {
                    erepD = 1;
                    btn_repD.Text = Convert.ToString(erepD);
                    btn4select = true;
                }
                if ((btn4select == false && btn1select == true && btn2select == false && btn3select == false)
                    || (btn4select == false && btn1select == false && btn2select == true && btn3select == false)
                    || (btn4select == false && btn1select == false && btn2select == false && btn3select == true))
                {
                    erepD = 2;
                    btn_repD.Text = Convert.ToString(erepD);
                    btn4select = true;
                }
                if ((btn4select == false && btn1select == true && btn2select == true && btn3select == false)
                    || (btn4select == false && btn1select == false && btn2select == true && btn3select == true)
                    || (btn4select == false && btn1select == true && btn2select == false && btn3select == true))
                {
                    erepD = 3;
                    btn_repD.Text = Convert.ToString(erepD);
                    btn4select = true;
                }
                if (btn4select == false && btn1select == true && btn2select == true && btn3select == true)
                {
                    erepD = 4;
                    btn_repD.Text = Convert.ToString(erepD);
                    btn4select = true;
                }
                verif_init();
            }
            else
            {
                //controle de la bonne reponse:
                if (bonneReponse == (int)bonneRep.D)
                {
                    repCorrecte();
                }
                else
                {
                    repFausse();
                }
            }//fin de else
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void recommencerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            reinit();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2.ActiveForm.Show();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (jeuChronometre == true)
            {
                if (lblChrono.Visible == true)
                {
                    chrono--;
                    lblChrono.Text = Convert.ToString(chrono / 10);
                }

                if (numJoueur == 1 && chrono <= 0)
                {
                    p1.deplPerso(false);
                    lbl_player1.Location = new Point(cases[p1.numLabEnCours].Location.X + cases[p1.numLabEnCours].Width / 10, cases[p1.numLabEnCours].Location.Y + cases[p1.numLabEnCours].Height / 8);
                    lbl_player1.BackColor = cases[p1.numLabEnCours].BackColor;
                    p1.lblEnCours = cases[p1.numLabEnCours];
                    brepj1 = 0;
                    numJoueur = 2;
                    lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();
                    recupererCategorie(p2.lblEnCours.BackColor);
                    genererQuestion(categQuestion);
                    reponsesBug = false;
                    replaceBtnRep();
                }
                else if (numJoueur == 2 && chrono <= 0)
                {
                    p2.deplPerso(false);
                    lbl_player2.Location = new Point(cases[p2.numLabEnCours].Location.X + cases[p2.numLabEnCours].Width / 10, cases[p2.numLabEnCours].Location.Y + cases[p2.numLabEnCours].Height / 8);
                    lbl_player2.BackColor = cases[p2.numLabEnCours].BackColor;
                    p2.lblEnCours = cases[p2.numLabEnCours];
                    brepj2 = 0;
                    numJoueur = 1;
                    lbl_tourJoueur.Text = "Joueur " + numJoueur.ToString();
                    recupererCategorie(p1.lblEnCours.BackColor);
                    genererQuestion(categQuestion);
                    reponsesBug = false;
                    replaceBtnRep();
                }
            }
            else { }           
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (modeBug == true && reponsesBug == true)
            {
                bugQuestion2();
            }
            else { }
        }        

    }
}