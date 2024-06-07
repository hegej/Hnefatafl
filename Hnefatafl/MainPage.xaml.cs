using Hnefatafl.Services;

namespace Hnefatafl
{
    public partial class MainPage : ContentPage
    {
        private GameState gameState;

        public MainPage()
        {
            InitializeComponent();
            gameState = new GameState();
            InitializeGameBoard();
        }

        private void InitializeGameBoard()
        {
            Grid gameGrid = this.FindByName<Grid>("GameBoard");
            for (int row = 0; row < 11; row++)
            {
                for (int column = 0; column < 11; column++)
                {
                    Button button = new Button
                    {
                        WidthRequest = 40,
                        HeightRequest = 40,
                        Margin = new Thickness(1),
                        BackgroundColor = Colors.White
                    };
                    button.Clicked += OnGameButtonClick;
                    gameGrid.Add(button, column, row);
                }
            }

            UpdateBoardUI();
        }

        private void UpdateBoardUI()
        {
            Grid gameGrid = this.FindByName<Grid>("GameBoard");
            for (int row = 0; row < 11; row++)
            {
                for (int column = 0; column < 11; column++)
                {
                    Button button = (Button)gameGrid.Children.Single(c => Grid.GetRow(c) == row && Grid.GetColumn(c) == column);
                    var piece = gameState.Board[row, column];
                    button.BackgroundColor = piece.PieceColor;
                    button.Text = piece.Type == PieceType.Empty ? "" : (piece.Type == PieceType.King ? "K" : "W");
                }
            }
        }

        private void OnGameButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            var row = Grid.GetRow(clickedButton);
            var column = Grid.GetColumn(clickedButton);

            if (gameState.TryMove(row, column, out var newColor))
            {
                clickedButton.BackgroundColor = newColor;
            }
            else
            {
                DisplayAlert("Invalid Move", "Please try a different move.", "OK");
            }
        }
    }

}
