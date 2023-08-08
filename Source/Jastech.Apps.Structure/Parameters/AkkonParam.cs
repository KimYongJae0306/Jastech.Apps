using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jastech.Apps.Structure.Parameters
{
    public class AkkonParam
    {
        [JsonProperty]
        public List<AkkonGroup> GroupList { get; set; } = new List<AkkonGroup>();

        [JsonProperty]
        public AkkonAlgoritmParam AkkonAlgoritmParam { get; set; } = new AkkonAlgoritmParam();

        private void AddGroup()
        {
            if(GroupList.Count() > 0)
            {
                int newName = GroupList.Count();
                var newGroup = GroupList[GroupList.Count() - 1].DeepCopy();
                newGroup.Index = newName;

                GroupList.Add(newGroup);
            }
            else
            {
                var newGroup = new AkkonGroup();
                newGroup.Index = 0;
                GroupList.Add(newGroup);
            }
        }

        public void AddGroup(int count)
        {
            for (int i = 0; i < count; i++)
                AddGroup();
        }

        public void CreateGroup(int groupCount)
        {
            for (int i = 0; i < groupCount; i++)
                AddGroup();
        }

        public void AdjustGroupCount(int newGroupCount)
        {
            if (GroupList.Count() == newGroupCount)
                return;

            if(GroupList.Count() < newGroupCount)
            {
                int newCount = newGroupCount - GroupList.Count();
                AddGroup(newCount);
            }
            else
            {
                int deleteCount = GroupList.Count() - newGroupCount;

                for (int i = 0; i < deleteCount; i++)
                {
                    DeleteGroup(GroupList.Count - 1);
                }
            }
        }

        public void DeleteGroup(int index)
        {
            GroupList.RemoveAt(index);
        }

        public void DeleteGroup(string name)
        {
            var group = GroupList.Where(x => x.Index == Convert.ToInt32(name)).FirstOrDefault();
            if (group != null)
                GroupList.Remove(group);
        }

        public AkkonGroup GetAkkonGroup(int index)
        {
            if (GroupList.Count() <= 0)
                return null;
            return GroupList.Where(x => x.Index == index).FirstOrDefault();
        }

        public void SetAkkonGroup(int index, AkkonGroup newGroupParam)
        {
            var group = GetAkkonGroup(index);
            if (group != null)
                group = newGroupParam;
        }

        public List<AkkonROI> GetAkkonROIList()
        {
            List<AkkonROI> roiList = new List<AkkonROI>();

            for (int i = 0; i < GroupList.Count; i++)
            {
                roiList.AddRange(GetAkkonGroup(i).AkkonROIList);
            }
            return roiList;
        }

        public AkkonParam DeepCopy()
        {
            return JsonConvertHelper.DeepCopy(this) as AkkonParam;
        }

        public void Dispose()
        {
            GroupList.ForEach(x => x.Dispose());
            GroupList.Clear();

            AkkonAlgoritmParam.Dispose();
        }
    }
}
