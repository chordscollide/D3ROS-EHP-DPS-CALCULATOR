using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diablo3_Stats {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  /// 
  public partial class MainWindow : Window {
      double level;
      string lifeperstr;
      string plifeperstr;
      string dodgestr;
      string blockstr;
      int plife;
      int parmor;
      int presall;
      double lifeper;
      double plifeper;
      double dodge;
      double block;
      double vitality;
      double armor;
      double blockhi;
      double blocklo;
      double physres;
      double coldres;
      double fireres;
      double lightres;
      double poisres;
      double holyres;
      int toughness;

    public MainWindow() {
      InitializeComponent();
    }

    private void calculate() {
      //EHP 
      level = Convert.ToDouble(LEVEL.Text);
      lifeperstr = Convert.ToString(LIFE.Text);
      plifeperstr = Convert.ToString(PLIFEPERCENT.Text);
      dodgestr = Convert.ToString(DODGE.Text);
      blockstr = Convert.ToString(BLOCK.Text);
      lifeper = Convert.ToDouble(lifeperstr.Remove(lifeperstr.Length - 1));
      plifeper = Convert.ToDouble(plifeperstr.Remove(plifeperstr.Length - 1));
      dodge = Convert.ToDouble(dodgestr.Remove(dodgestr.Length - 1));
      block = Convert.ToDouble(blockstr.Remove(blockstr.Length - 1));
      vitality = Convert.ToDouble(VITALITY.Text);
      armor = Convert.ToDouble(ARMOR.Text);
      parmor = Convert.ToInt32(PARMOR.Text);
      blockhi = Convert.ToDouble(BLOCKHI.Text);
      blocklo = Convert.ToDouble(BLOCKLO.Text);
      physres = Convert.ToDouble(PHYS.Text);
      coldres = Convert.ToDouble(COLD.Text);
      fireres = Convert.ToDouble(FIRE.Text);
      lightres = Convert.ToDouble(LIGHT.Text);
      poisres = Convert.ToDouble(POIS.Text);
      holyres = Convert.ToDouble(HOLY.Text);
      double armorp = armor * (1 + ((double)parmor * 0.50 / 100));
      double armorpp = armor * (1 + (((double)parmor * 0.50 + 0.50) / 100));

      //All Resist
      double allres = (physres + coldres + fireres + lightres + poisres + holyres) / 6;
      double resistdr = ((presall*5 + allres) / ((5 * level) + (presall * 5 + allres)));
      double resistdrp = ((allres + presall*5+5) / ((5 * level) + (allres+presall*5+5)));

      //Armor
      double armordr = (armorp / ((50 * level) + armorp));
      double armordrp = (armorpp / ((50 * level) + armorpp));


      //Life
      double life = Math.Floor((vitality * 80 + 36 + level * 4) * (1 + (lifeper + plifeper) / 100));
      double lifep = Math.Floor((vitality * 80 + 36 + level * 4) * (1 + (lifeper + plifeper + 0.50) / 100));

      //DR
      double dr = 1 - ((1 - dodge / 100) * (1 - armordr) * (1 - resistdr));
      double drp = 1 - ((1 - dodge / 100) * (1 - armordrp) * (1 - resistdr));
      double drr = 1 - ((1 - dodge / 100) * (1 - armordr) * (1 - resistdrp));

      //Shield EHP
      double shield = (blockhi + blocklo) * (block / 100);

      //Text Append
      ALLRES.Text = Convert.ToString(Math.Round(allres*10)/10);
      ALLRESDR.Text = Convert.ToString(Math.Round(resistdr * 10000) / 100) + "%";
      ARMORDR.Text = Convert.ToString(Math.Round(armordr * 10000) / 100) + "%";
      MAXHP.Text = Convert.ToString(life);
      ALLDR.Text = Convert.ToString(Math.Round(dr * 10000) / 100) + "%";
      toughness = (int)(Math.Floor((life + shield) / (1 - dr)));
      int pltoughness = (int)(Math.Floor((lifep + shield) / (1 - dr)));
      int patoughness = (int)(Math.Floor((life + shield) / (1 - drp)));
      int pratoughness = (int)(Math.Floor((life + shield) / (1 - drr)));
      PARMOREHP.Text = Convert.ToString(patoughness - toughness);
      PLIFEEHP.Text = Convert.ToString(pltoughness - toughness);
      PRESALLEHP.Text = Convert.ToString(pratoughness - toughness);
      TOUGHNESS.Text = Convert.ToString(toughness);
    }

    private void Button_Click(object sender, RoutedEventArgs e) {
      calculate();
    }

    private void Plife_PlusOne(object sender, RoutedEventArgs e) {
      plife = Convert.ToInt32(PLIFE.Text); 
      plife += 1;
      PLIFEPERCENT.Text = Convert.ToString(plife * 0.50) + "%";
      PLIFE.Text = Convert.ToString(plife);
      calculate();
    }

    private void Plife_MinusOne(object sender, RoutedEventArgs e) {
      plife = Convert.ToInt32(PLIFE.Text);
      plife -= 1;
      PLIFEPERCENT.Text = Convert.ToString(plife * 0.50) + "%";
      PLIFE.Text = Convert.ToString(plife);
      calculate();
    }

    private void Parmor_PlusOne(object sender, RoutedEventArgs e) {
      parmor = Convert.ToInt32(PARMOR.Text);
      parmor += 1;
      PARMORPERCENT.Text = Convert.ToString(parmor * 0.50) + "%";
      PARMOR.Text = Convert.ToString(parmor);
      calculate();
    }

    private void Parmor_MinusOne(object sender, RoutedEventArgs e) {
      parmor = Convert.ToInt32(PARMOR.Text);
      parmor -= 1;
      PARMORPERCENT.Text = Convert.ToString(parmor * 0.50) + "%";
      PARMOR.Text = Convert.ToString(parmor);
      calculate();
    }

    private void Presall_PlusOne(object sender, RoutedEventArgs e) {
      presall = Convert.ToInt32(PRESALL.Text);
      presall += 1;
      PRESALLSTAT.Text = Convert.ToString(presall * 5);
      PRESALL.Text = Convert.ToString(presall);
      calculate();
    }

    private void Presall_MinusOne(object sender, RoutedEventArgs e) {
      presall = Convert.ToInt32(PRESALL.Text);
      presall -= 1;
      PRESALLSTAT.Text = Convert.ToString(presall * 5);
      PRESALL.Text = Convert.ToString(presall);
      calculate();
    }
  }
}
