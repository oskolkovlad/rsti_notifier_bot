﻿namespace RstiNotifierBot.Interfaces.BusinessComponents
{
    using RstiNotifierBot.Dto;

    internal interface IBCNews
    {
        NewsDto GetLastNewsItem();
    }
}
