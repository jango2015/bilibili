﻿using bilibili.Http;
using bilibili.Methods;
using bilibili.Models;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace bilibili.Views.PartViews
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Music : Page
    {
        public Music()
        {
            this.InitializeComponent();
        }
        private void GridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            GridView gr = sender as GridView;
            Content content = gr.SelectedItem as Content;
            if (content != null)
            {
                Frame.Navigate(typeof(Detail_P), content.Num, new Windows.UI.Xaml.Media.Animation.DrillInNavigationTransitionInfo());
            }
        }

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pivot pivot = sender as Pivot;
            TextBlock txt = this.FindName(string.Format("h{0}", pivot.SelectedIndex)) as TextBlock;
            for (int i = 0; i < pivot.Items.Count; i++)
            {
                TextBlock temp = this.FindName(string.Format("h{0}", i)) as TextBlock;
                temp.Foreground = new SolidColorBrush(Colors.LightGray);
            }
            txt.Foreground = new SolidColorBrush(Colors.White);
            switch (pivot.SelectedIndex)
            {
                case 0:
                    {
                        if (gv_hot.Items.Count == 0)
                        {
                            var temp = await ContentServ.GetContentAsync(3, 1);
                            foreach (var item in temp)
                            {
                                gv_hot.Items.Add(item);
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        if (gv_ori.Items.Count == 0)
                        {
                            var temp = await ContentServ.GetContentAsync(28, 1);
                            foreach (var item in temp)
                            {
                                gv_ori.Items.Add(item);
                            }
                        }
                    }
                    break;
                case 2:
                    {
                        if (gv_resing.Items.Count == 0)
                        {
                            var temp = await ContentServ.GetContentAsync(31, 1);
                            foreach (var item in temp)
                            {
                                gv_resing.Items.Add(item);
                            }
                        }
                    }
                    break;
                case 3:
                    {
                        if (gv_vo.Items.Count == 0)
                        {
                            var temp = await ContentServ.GetContentAsync(30, 1);
                            foreach (var item in temp)
                            {
                                gv_vo.Items.Add(item);
                            }
                        }
                    }
                    break;
                case 4:
                    {
                        if (gv_play.Items.Count == 0)
                        {
                            var temp = await ContentServ.GetContentAsync(59, 1);
                            foreach (var item in temp)
                            {
                                gv_play.Items.Add(item);
                            }
                        }
                    }
                    break;
                case 5:
                    {
                        if (gv_third.Items.Count == 0)
                        {
                            var temp = await ContentServ.GetContentAsync(29, 1);
                            foreach (var item in temp)
                            {
                                gv_third.Items.Add(item);
                            }
                        }
                    }
                    break;
                case 6:
                    {
                        if (gv_oped.Items.Count == 0)
                        {
                            var temp = await ContentServ.GetContentAsync(54, 1);
                            foreach (var item in temp)
                            {
                                gv_oped.Items.Add(item);
                            }
                        }
                    }
                    break;
                case 7:
                    {
                        if (gv_album.Items.Count == 0)
                        {
                            var temp = await ContentServ.GetContentAsync(130, 1);
                            foreach (var item in temp)
                            {
                                gv_album.Items.Add(item);
                            }
                        }
                    }
                    break;
            }
        }
        bool isLoading = false;
        private void GridView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            GridView gridview = sender as GridView;
            var scroll = Load.FindChildOfType<ScrollViewer>(gridview);
            var text = Load.FindChildOfType<TextBlock>(gridview);
            scroll.ViewChanged += async (s, a) =>
            {
                if ((scroll.VerticalOffset >= scroll.ScrollableHeight - 50 || scroll.ScrollableHeight == 0) && !isLoading)
                {
                    text.Visibility = Visibility.Visible;
                    int count0 = gridview.Items.Count;
                    int page = gridview.Items.Count / 20 + 1;
                    isLoading = true;
                    List<Content> temps = await ContentServ.GetContentAsync(int.Parse(gridview.Tag.ToString()), page);
                    if (temps.Count == 0)
                    {
                        text.Text = "装填完毕！";
                        return;
                    }
                    text.Visibility = Visibility.Collapsed;
                    foreach (var item in temps)
                    {
                        gridview.Items.Add(item);
                    }
                    isLoading = false;
                }
            };
        }
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            width.Width = WidthFit.GetWidth(ActualWidth, 200, 160, 0);
        }
    }
}
