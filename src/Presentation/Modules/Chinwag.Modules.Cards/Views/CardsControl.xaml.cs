using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Chinwag.Modules.Cards.Views
{
    /// <summary>
    /// Interaction logic for CardsControl.xaml
    /// </summary>
    public partial class CardsControl : UserControl
    {
        public ObservableCollection<string> ImagePaths => new ObservableCollection<string>
        {
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            "/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
            //"/Chinwag.Modules.Cards;component/Views/defaut-card.png",
        };

        public CardsControl()
        {
            InitializeComponent();
            this.DataContext = ImagePaths;
        }
    }
}
