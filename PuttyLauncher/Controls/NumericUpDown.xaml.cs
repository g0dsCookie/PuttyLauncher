using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CookieProjects.PuttyLauncher.Controls
{
	/// <summary>
	/// Interaction logic for NumericUpDown.xaml
	/// </summary>
	public partial class NumericUpDown : UserControl
    {
		#region DependencyProperties

		public static readonly DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown),
				new PropertyMetadata(0, new PropertyChangedCallback(OnSomeValuePropertyChanged)));

		private static void OnSomeValuePropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
		{
			var numericUpDown = target as NumericUpDown;
			numericUpDown.tbValue.Text = e.NewValue.ToString();
		}

		public static readonly DependencyProperty MaximumProperty =
			DependencyProperty.Register("Maximum", typeof(int), typeof(NumericUpDown), new UIPropertyMetadata(int.MaxValue));

		public static readonly DependencyProperty MinimumProperty =
			DependencyProperty.Register("Minimum", typeof(int), typeof(NumericUpDown), new UIPropertyMetadata(0));

		private static readonly RoutedEvent ValueChangedEvent =
			EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumericUpDown));

		private static readonly RoutedEvent IncreaseClickedEvent =
			EventManager.RegisterRoutedEvent("IncreaseClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumericUpDown));

		private static readonly RoutedEvent DecreaseClickedEvent =
			EventManager.RegisterRoutedEvent("DecreaseClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NumericUpDown));

		#endregion

		#region Properties

		public int Value
		{
			get
			{
				return (int)GetValue(ValueProperty);
			}
			set
			{
				tbValue.Text = value.ToString();
				SetValue(ValueProperty, value);
			}
		}

		public int Maximum
		{
			get { return (int)GetValue(MaximumProperty); }
			set { SetValue(MaximumProperty, value); }
		}

		public int Minimum
		{
			get { return (int)GetValue(MinimumProperty); }
			set { SetValue(MinimumProperty, value); }
		}

		public event RoutedEventHandler ValueChanged
		{
			add { AddHandler(ValueChangedEvent, value); }
			remove { RemoveHandler(ValueChangedEvent, value); }
		}

		public event RoutedEventHandler IncreaseClicked
		{
			add { AddHandler(IncreaseClickedEvent, value); }
			remove { RemoveHandler(IncreaseClickedEvent, value); }
		}

		public event RoutedEventHandler DecreaseClicked
		{
			add { AddHandler(DecreaseClickedEvent, value); }
			remove { RemoveHandler(DecreaseClickedEvent, value); }
		}

		#endregion

		private readonly Regex NumericMatch = new Regex(@"^-?\d+$");

        public NumericUpDown()
        {
            InitializeComponent();

			this.tbValue.Text = "0";
        }

		private void ResetText(TextBox textBox)
		{
			textBox.Text = Minimum.ToString();
			textBox.SelectAll();
		}

		private void tbValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			var tb = (TextBox)sender;
			var text = tb.Text.Insert(tb.CaretIndex, e.Text);

			e.Handled = !NumericMatch.IsMatch(text);
		}

		private void tbValue_TextChanged(object sender, TextChangedEventArgs e)
		{
			var tb = (TextBox)sender;
			if (!NumericMatch.IsMatch(tb.Text)) ResetText(tb);
			Value = Convert.ToInt32(tb.Text);
			if (Value < Minimum) Value = Minimum;
			else if (Value > Maximum) Value = Maximum;

			RaiseEvent(new RoutedEventArgs(ValueChangedEvent));
		}

		private void tbValue_PreviewKeyUp(object sender, KeyEventArgs e)
		{
			if (!e.IsDown) return;
			if (e.Key == Key.Up && Value < Maximum)
			{
				Value++;
				RaiseEvent(new RoutedEventArgs(IncreaseClickedEvent));
			}
			else if (e.Key == Key.Down && Value > Minimum)
			{
				Value--;
				RaiseEvent(new RoutedEventArgs(DecreaseClickedEvent));
			}
		}

		private void Increase_Click(object sender, RoutedEventArgs e)
		{
			if (Value < Maximum)
			{
				Value++;
				RaiseEvent(new RoutedEventArgs(IncreaseClickedEvent));
			}
		}

		private void Decrease_Click(object sender, RoutedEventArgs e)
		{
			if (Value > Minimum)
			{
				Value--;
				RaiseEvent(new RoutedEventArgs(DecreaseClickedEvent));
			}
		}
	}
}
