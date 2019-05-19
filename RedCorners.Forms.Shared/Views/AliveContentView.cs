﻿using RedCorners.Forms.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public class AliveContentView : ContentView
    {
        bool parentWasNull = true;
        WeakReference<AliveContentPage> pagePointer = null;

        public bool FixTopPadding
        {
            get => (bool)GetValue(FixTopPaddingProperty);
            set => SetValue(FixTopPaddingProperty, value);
        }

        public bool FixBottomPadding
        {
            get => (bool)GetValue(FixBottomPaddingProperty);
            set => SetValue(FixBottomPaddingProperty, value);
        }

        public static BindableProperty FixTopPaddingProperty = BindableProperty.Create(
            nameof(FixTopPadding),
            typeof(bool),
            typeof(AliveContentView),
            false,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as AliveContentView).AdjustPadding();
            });

        public static BindableProperty FixBottomPaddingProperty = BindableProperty.Create(
            nameof(FixBottomPadding),
            typeof(bool),
            typeof(AliveContentView),
            false,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldVal, newVal) =>
            {
                (bindable as AliveContentView).AdjustPadding();
            });

        Thickness? originalPadding;

        void AdjustPadding()
        {
            if (originalPadding == null)
            {
                originalPadding = Padding;
            }

            var pageMargin = NotchSystem.Instance.GetPageMargin();
            var top = FixTopPadding ? pageMargin.Top : originalPadding.Value.Top;
            var bottom = FixBottomPadding ? pageMargin.Bottom : originalPadding.Value.Bottom;
            Padding = new Thickness(
                originalPadding.Value.Left,
                top,
                originalPadding.Value.Right,
                bottom);
        }

        void TryHook(AliveContentPage page)
        {
            if (parentWasNull && Parent != null)
            {
                page = page ?? GetPage();
                if (page != null)
                {
                    parentWasNull = false;
                    pagePointer = new WeakReference<AliveContentPage>(page);
                    page.OnAppeared += Page_OnAppeared;
                    page.OnDisappeared += Page_OnDisappeared;
                    TriggerStart();
                }
            }
            else if (!parentWasNull && Parent == null)
            {
                if (pagePointer.TryGetTarget(out var lastPage))
                {
                    lastPage.OnDisappeared -= Page_OnDisappeared;
                    lastPage.OnAppeared -= Page_OnAppeared;
                }
                parentWasNull = true;
                TriggerStop();
            }

            AdjustPadding();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            TryHook(null);
        }

        public void HookToAlivePage(AliveContentPage page)
        {
            TryHook(page);
        }

        protected override void ChangeVisualState()
        {
            base.ChangeVisualState();
        }

        private void Page_OnDisappeared(object sender, EventArgs e)
        {
            TriggerStop();
        }

        private void Page_OnAppeared(object sender, EventArgs e)
        {
            TriggerStart();
        }

        public virtual void OnStart()
        {

        }

        public virtual void OnStop()
        {

        }

        bool isStarted = false;
        public void TriggerStart()
        {
            if (!isStarted)
            {
                isStarted = true;
                OnStart();
            }
        }

        public void TriggerStop()
        {
            if (isStarted)
            {
                isStarted = false;
                OnStop();
            }
        }

        public AliveContentPage GetPage()
        {
            if (Parent is AliveContentPage p) return p;

            var el = this as Element;
            while (true)
            {
                if (el == null || el.Parent == null) return null;
                if (el.Parent is AliveContentPage page) return page;
                el = el.Parent;
            }
        }

        public bool GetIsVisibleRecursive()
        {
            var el = (View)this;
            while (el != null)
            {
                if (!el.IsVisible) return false;
                el = el.Parent as View;
            }
            return true;
        }

        WeakReference<BindableModel> lastContext = null;
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (lastContext != null && lastContext.TryGetTarget(out var lastVm))
                lastVm?.OnUnbind(this);
            if (BindingContext is BindableModel vm)
            {
                vm.OnBind(this);
                lastContext = new WeakReference<BindableModel>(vm);
            }
        }
    }
}
