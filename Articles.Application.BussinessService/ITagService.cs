﻿using System;
using System.Collections.Generic;
using Articles.DomainModel;
using FrameWork.Application;
using FrameWork.Core;

namespace Articles.Application.BussinessService
{
    public interface ITagService:IService
    {
        Tag Get(int id);
        EntityAction Create(Tag entity);

        EntityAction Update(Tag entity);

        EntityAction Delete(int id);

        IEnumerable<Tag> Where(System.Linq.Expressions.Expression<Func<Tag,bool>> perdicate);

    }
}