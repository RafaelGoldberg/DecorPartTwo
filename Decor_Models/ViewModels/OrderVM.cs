﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tzedakah_Models.ViewModels
{
    public class InquiryVM
    {
        public InquiryHeader InquiryHeader { get; set; }
        public IEnumerable<InquiryDetail> InquiryDetail { get; set; }
    }
}
