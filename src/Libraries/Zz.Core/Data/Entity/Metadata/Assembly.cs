using System;
using System.Collections.Generic;
using Zz.Core.Data.Entity.Grpc;
using Zz.Core.Data.Entity.Media;

namespace Zz.Core.Data.Entity.Metadata
{
    public class Assembly : BaseEntity
    {
        private ICollection<ServiceInfo> _serviceInfos;

        public string Name { get; set; }
        public string Filename { get; set; }
        public Guid FileInfoId { get; set; }

        public bool Deleted { get; set; }

        //TODO
        /*
         * dll的版本号
         * dll的最后修改时间
         * Assembly记录的最后上传时间
         * （是否支持 先创建Assembly 后上传dll？可用于关系图预览）
         * 
         */

        #region Nav properties
        public virtual FileInfo FileInfo { get; set; }

        public virtual ICollection<ServiceInfo> ServiceInfos
        {
            get => _serviceInfos ?? (_serviceInfos = new List<ServiceInfo>());
            protected set => _serviceInfos = value;
        }
        #endregion
        // Todo
    }
}
