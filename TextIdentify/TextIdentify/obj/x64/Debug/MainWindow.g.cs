﻿#pragma checksum "..\..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "98B8A8AEC9EA21F7E764FD731466E92B597E66F50B72AB3DF38D36CCC003B5D6"
//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TextIdentify;


namespace TextIdentify {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MainFileName_textbox;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button MainFileSelect_button;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Xcoordi_texbox;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Ycoordi_texbox;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Width_texbox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Height_texbox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button IdentifyText_button;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OCR_Result_textobx;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Display_image;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas RedRect_canvas;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle IdentifyRange_rect;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TextIdentify;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\MainWindow.xaml"
            ((TextIdentify.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainFileName_textbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.MainFileSelect_button = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\MainWindow.xaml"
            this.MainFileSelect_button.Click += new System.Windows.RoutedEventHandler(this.MainFileSelect_button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Xcoordi_texbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 33 "..\..\..\MainWindow.xaml"
            this.Xcoordi_texbox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Number_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Ycoordi_texbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\..\MainWindow.xaml"
            this.Ycoordi_texbox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Number_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Width_texbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 41 "..\..\..\MainWindow.xaml"
            this.Width_texbox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Number_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Height_texbox = ((System.Windows.Controls.TextBox)(target));
            
            #line 44 "..\..\..\MainWindow.xaml"
            this.Height_texbox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.Number_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 8:
            this.IdentifyText_button = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\MainWindow.xaml"
            this.IdentifyText_button.Click += new System.Windows.RoutedEventHandler(this.IdentifyText_button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.OCR_Result_textobx = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.Display_image = ((System.Windows.Controls.Image)(target));
            
            #line 57 "..\..\..\MainWindow.xaml"
            this.Display_image.SizeChanged += new System.Windows.SizeChangedEventHandler(this.Display_image_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.RedRect_canvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 12:
            this.IdentifyRange_rect = ((System.Windows.Shapes.Rectangle)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

