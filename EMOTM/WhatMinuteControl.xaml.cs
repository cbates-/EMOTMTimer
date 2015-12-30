using System.Windows;
using System.Windows.Controls;

namespace EMOTM
{
	/// <summary>
	/// Interaction logic for WhatMinuteControl.xaml
	/// </summary>
	public partial class WhatMinuteControl : UserControl
	{

		/// <summary>
		/// The <see cref="ThisName" /> dependency property's name.
		/// </summary>
		public const string NamePropertyName = "ThisName";

		/// <summary>
		/// Gets or sets the value of the <see cref="ThisName" />
		/// property. This is a dependency property.
		/// </summary>
		public string ThisName
		{
			get
			{
				return (string)GetValue(ThisNameProperty);
			}
			set
			{
				SetValue(ThisNameProperty, value);
			}
		}

		/// <summary>
		/// Identifies the <see cref="ThisName" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ThisNameProperty = DependencyProperty.Register(
			NamePropertyName,
			typeof(string),
			typeof(WhatMinuteControl),
			new PropertyMetadata(null));
		
		public WhatMinuteControl()
		{
			this.InitializeComponent();

			//LayoutRoot.DataContext = this;
		}
	}
}