using Jastech.Framework.Macron.Akkon.Parameters;
using Jastech.Framework.Util.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Structure.Parameters
{
    public class AkkonParam
    {
        [JsonProperty]
        public List<MacronAkkonGroup> GroupList { get; set; } = new List<MacronAkkonGroup>();

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
                var newGroup = new MacronAkkonGroup();
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

                for (int i = GroupList.Count(); i > deleteCount; i--)
                {
                    DeleteGroup(i);
                }
            }
        }

        public void DeleteGroup(int index)
        {
            GroupList.RemoveAt(index);
        }

        public void DeleteGroup(string name)
        {
            var group = GroupList.Where(x => x.Index == Convert.ToInt32(name)).First();
            if (group != null)
                GroupList.Remove(group);
        }

        public MacronAkkonGroup GetAkkonGroup(int index)
        {
            if (GroupList.Count() <= 0)
                return null;
            return GroupList.Where(x => x.Index == index).First();
        }

        public void SetAkkonGroup(int index, MacronAkkonGroup newGroupParam)
        {
            var group = GetAkkonGroup(index);
            if (group != null)
                group = newGroupParam;
        }

        public AkkonParam DeepCopy()
        {
            return JsonConvertHelper.DeepCopy(this) as AkkonParam;
        }
    }
}
