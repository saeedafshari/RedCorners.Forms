﻿using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RedCorners.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty("ToolBar")]
    public partial class TitleBar 
    {
        public TitleBar()
        {
            InitializeComponent();
            UpdateBackButtonImage();
            backImage.InputTransparent = true;

            var tap = new TapGestureRecognizer();
            tap.Tapped += Tap_Tapped;
            controlButtons.GestureRecognizers.Add(tap);

            UpdateToolBar();
            contentContainer.FixTopPadding = FixTopPadding;
            contentContainer.FixBottomPadding = FixBottomPadding;
        }

        private void Tap_Tapped(object sender, EventArgs e)
        {
            if (BackCommand?.CanExecute(BackCommandParameter) ?? false)
                BackCommand.Execute(BackCommandParameter);
        }

        public View Background
        {
            get => (View)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public View ToolBar
        {
            get => (View)GetValue(ToolBarProperty);
            set => SetValue(ToolBarProperty, value);
        }

        public double ContentHeightRequest
        {
            get => (double)GetValue(ContentHeightRequestProperty);
            set => SetValue(ContentHeightRequestProperty, value);
        }

        public Thickness ContentMargin
        {
            get => (Thickness)GetValue(ContentMarginProperty);
            set => SetValue(ContentMarginProperty, value);
        }

        public bool? IsBackButtonVisible
        {
            get => (bool?)GetValue(IsBackButtonVisibleProperty);
            set => SetValue(IsBackButtonVisibleProperty, value);
        }

        public ImageSource CustomBackImage
        {
            get => (ImageSource)GetValue(CustomBackImageProperty);
            set => SetValue(CustomBackImageProperty, value);
        }

        public ICommand BackCommand
        {
            get => (ICommand)GetValue(BackCommandProperty);
            set => SetValue(BackCommandProperty, value);
        }

        public object BackCommandParameter
        {
            get => GetValue(BackCommandParameterProperty);
            set => SetValue(BackCommandParameterProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        public new bool FixTopPadding
        {
            get => (bool)GetValue(FixTopPaddingProperty);
            set => SetValue(FixTopPaddingProperty, value);
        }

        public new bool FixBottomPadding
        {
            get => (bool)GetValue(FixBottomPaddingProperty);
            set => SetValue(FixBottomPaddingProperty, value);
        }

        public static readonly BindableProperty BackgroundProperty = BindableProperty.Create(
            propertyName: nameof(Background),
            returnType: typeof(View),
            declaringType: typeof(TitleBar),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
            });

        public static readonly BindableProperty ToolBarProperty = BindableProperty.Create(
            propertyName: nameof(ToolBar),
            returnType: typeof(View),
            declaringType: typeof(TitleBar),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitleBar titlebar)
                    titlebar.UpdateToolBar();
            });

        public static readonly BindableProperty ContentHeightRequestProperty = BindableProperty.Create(
            propertyName: nameof(ContentHeightRequest),
            returnType: typeof(double),
            declaringType: typeof(TitleBar),
            defaultValue: 60.0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {

            });

        public static readonly BindableProperty ContentMarginProperty = BindableProperty.Create(
            propertyName: nameof(ContentMargin),
            returnType: typeof(Thickness),
            declaringType: typeof(TitleBar),
            defaultValue: default(Thickness),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {

            });

        public static readonly BindableProperty IsBackButtonVisibleProperty = BindableProperty.Create(
            propertyName: nameof(IsBackButtonVisible),
            returnType: typeof(bool?),
            declaringType: typeof(TitleBar),
            defaultValue: default(bool?),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as TitleBar)?.UpdateBackButtonImage();
            });

        public static readonly BindableProperty CustomBackImageProperty = BindableProperty.Create(
            propertyName: nameof(CustomBackImage),
            returnType: typeof(ImageSource),
            declaringType: typeof(TitleBar),
            defaultValue: default(ImageSource),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as TitleBar)?.UpdateBackButtonImage();
            });

        public static readonly BindableProperty BackCommandProperty = BindableProperty.Create(
            propertyName: nameof(BackCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(TitleBar),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {

            });

        public static readonly BindableProperty BackCommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(BackCommandParameter),
            returnType: typeof(object),
            declaringType: typeof(TitleBar),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {

            });

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(TitleBar),
            defaultValue: Color.White,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {

            });

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: nameof(FontSize),
            returnType: typeof(double),
            declaringType: typeof(TitleBar),
            defaultValue: 16.0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {

            });

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
            propertyName: nameof(FontFamily),
            returnType: typeof(string),
            declaringType: typeof(TitleBar),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {

            });

        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
            propertyName: nameof(FontAttributes),
            returnType: typeof(FontAttributes),
            declaringType: typeof(TitleBar),
            defaultValue: FontAttributes.Bold,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {

            });

        public static new readonly BindableProperty FixTopPaddingProperty = BindableProperty.Create(
           nameof(FixTopPadding),
           typeof(bool),
           typeof(TitleBar),
           true,
           BindingMode.TwoWay,
           propertyChanged: (bindable, oldVal, newVal) =>
           {
               if (bindable is TitleBar titlebar && titlebar.contentContainer != null)
                   titlebar.contentContainer.FixTopPadding = (bool)newVal;
           });

        public static new readonly BindableProperty FixBottomPaddingProperty = BindableProperty.Create(
            nameof(FixBottomPadding),
            typeof(bool),
            typeof(TitleBar),
            false,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                if (bindable is TitleBar titlebar && titlebar.contentContainer != null)
                    titlebar.contentContainer.FixBottomPadding = (bool)newVal;
            });

        void UpdateBackButtonImage()
        {
            if (CustomBackImage != null)
            {
                backImage.Source = CustomBackImage;
                return;
            }
            var showBack = IsBackButtonVisible ?? (BindingContext is BindableModel vm ? vm.IsModal : true);
            if (showBack)
                backImage.Source = ImageSource.FromResource("RedCorners.Forms.leftarrow.png", typeof(TitleBar).GetTypeInfo().Assembly);
            else
                backImage.Source = ImageSource.FromResource("RedCorners.Forms.menu.png", typeof(TitleBar).GetTypeInfo().Assembly);
        }

        void UpdateToolBar()
        {
            buttons.Content = ToolBar;
        }
    }
}