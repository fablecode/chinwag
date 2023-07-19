using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace chinwag.Services;

public interface INavigationServiceEx
{
    event NavigatedEventHandler Navigated;
    event NavigationFailedEventHandler NavigationFailed;
    Frame Frame { get; set; }
    bool CanGoBack { get; }
    bool CanGoForward { get; }
    void GoBack();
    void GoForward();
    bool Navigate(Uri sourcePageUri, object extraData = null);
    bool Navigate(Type sourceType);
}