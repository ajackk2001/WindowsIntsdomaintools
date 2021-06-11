using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace DomainTool
{
    public partial class KeyDecoder : Component
    {
        public KeyDecoder()
        {
            InitializeComponent();
        }

        public KeyDecoder(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
