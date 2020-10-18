using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWorld.Web.Client.Shell.Components
{
    public partial class ExpandingTextArea : ComponentBase
    {
        private string _text;
        [Parameter]
        public string Value
        {
            get => _text;
            set
            {
                _text = value;
                CalculateSize(_text);
            }
        }
        [Parameter]
        public string Placeholder { get; set; }
        [Parameter]
        public int MinRows { get; set; } = 4;
        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }
        private int Rows { get; set; }

        private void CalculateSize(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Rows = Math.Max(text.Split('\n').Length, text.Split('\r').Length);
                Rows = Math.Max(Rows, MinRows);
            }
            else
            {
                Rows = MinRows;
            }
        }
    }
}
