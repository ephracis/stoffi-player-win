﻿using Microsoft.Xaml.Interactivity;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Stoffi.Win.Logic.Behaviors
{
    /// <summary>
    /// A behavior for setting the visual state based on a property of the ViewModel
    /// </summary>
    public class EnumStateBehavior : DependencyObject, IBehavior
    {
        public DependencyObject AssociatedObject { get; private set; }

        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(EnumStateBehavior),
            new PropertyMetadata(null, ValuePropertyChanged));

        private static void ValuePropertyChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as EnumStateBehavior;
            if (behavior.AssociatedObject == null || e.NewValue == null) return;

            VisualStateManager.GoToState(behavior.AssociatedObject as Control,
                e.NewValue.ToString(), true);
        }

        public void Attach(DependencyObject associatedObject)
        {
            var control = associatedObject as Control;
            if (control == null)
                throw new ArgumentException(
                    "EnumStateBehavior can be attached only to Control");

            AssociatedObject = associatedObject;
        }

        public void Detach()
        {
            AssociatedObject = null;
        }
    }
}
