﻿namespace AspNetCoreSerilogExample.Web.Services.Processing
{
    public interface IProcessOrder
    {
        bool ProcessOrder(string order);
    }
}