using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions;
using TaleWorlds.MountAndBlade;

namespace SkillTrainingMod.ViewModels
{
    public class SkillTrainingModNumericOptionsDataVM : ViewModel
    {
        // Token: 0x0600078E RID: 1934 RVA: 0x0001C96C File Offset: 0x0001AB6C
        public SkillTrainingModNumericOptionsDataVM(int min, int max,int intialValue,int incrementAmount, string name, TextObject description)
        {
            this._initialValue = intialValue;
            this.Min = min;
            this.Max = max;
            this.Name = name;
            this.DiscreteIncrementInterval = incrementAmount;
            this.UpdateContinuously = true;
            this.OptionValue = this._initialValue;
        }

        // Token: 0x0600078F RID: 1935 RVA: 0x0001CA00 File Offset: 0x0001AC00
        private string GetValueAsString()
        {
            string result = this._optionValue.ToString();
            return result;
        }

        // Token: 0x1700023F RID: 575
        // (get) Token: 0x06000790 RID: 1936 RVA: 0x0001CA8D File Offset: 0x0001AC8D
        // (set) Token: 0x06000791 RID: 1937 RVA: 0x0001CA95 File Offset: 0x0001AC95
        [DataSourceProperty]
        public float DiscreteIncrementInterval
        {
            get
            {
                return this._discreteIncrementInterval;
            }
            set
            {
                if (value != this._discreteIncrementInterval)
                {
                    this._discreteIncrementInterval = value;
                    base.OnPropertyChangedWithValue(value, "DiscreteIncrementInterval");
                }
            }
        }

        // Token: 0x17000240 RID: 576
        // (get) Token: 0x06000792 RID: 1938 RVA: 0x0001CAB3 File Offset: 0x0001ACB3
        // (set) Token: 0x06000793 RID: 1939 RVA: 0x0001CABB File Offset: 0x0001ACBB
        [DataSourceProperty]
        public float Min
        {
            get
            {
                return this._min;
            }
            set
            {
                if (value != this._min)
                {
                    this._min = value;
                    base.OnPropertyChangedWithValue(value, "Min");
                }
            }
        }

        // Token: 0x17000241 RID: 577
        // (get) Token: 0x06000794 RID: 1940 RVA: 0x0001CAD9 File Offset: 0x0001ACD9
        // (set) Token: 0x06000795 RID: 1941 RVA: 0x0001CAE1 File Offset: 0x0001ACE1
        [DataSourceProperty]
        public float Max
        {
            get
            {
                return this._max;
            }
            set
            {
                if (value != this._max)
                {
                    this._max = value;
                    base.OnPropertyChangedWithValue(value, "Max");
                }
            }
        }

        // Token: 0x17000242 RID: 578
        // (get) Token: 0x06000796 RID: 1942 RVA: 0x0001CAFF File Offset: 0x0001ACFF
        // (set) Token: 0x06000797 RID: 1943 RVA: 0x0001CB07 File Offset: 0x0001AD07
        [DataSourceProperty]
        public float OptionValue
        {
            get
            {
                return this._optionValue;
            }
            set
            {
                if (value != this._optionValue)
                {
                    this._optionValue = value;
                    base.OnPropertyChangedWithValue(value, "OptionValue");
                    base.OnPropertyChanged("OptionValueAsString");
                    this.UpdateValue();
                }
            }
        }

        // Token: 0x17000243 RID: 579
        // (get) Token: 0x06000798 RID: 1944 RVA: 0x0001CB36 File Offset: 0x0001AD36
        // (set) Token: 0x06000799 RID: 1945 RVA: 0x0001CB3E File Offset: 0x0001AD3E
        [DataSourceProperty]
        public bool IsDiscrete
        {
            get
            {
                return this._isDiscrete;
            }
            set
            {
                if (value != this._isDiscrete)
                {
                    this._isDiscrete = value;
                    base.OnPropertyChangedWithValue(value, "IsDiscrete");
                }
            }
        }

        // Token: 0x17000244 RID: 580
        // (get) Token: 0x0600079A RID: 1946 RVA: 0x0001CB5C File Offset: 0x0001AD5C
        // (set) Token: 0x0600079B RID: 1947 RVA: 0x0001CB64 File Offset: 0x0001AD64
        [DataSourceProperty]
        public bool UpdateContinuously
        {
            get
            {
                return this._updateContinuously;
            }
            set
            {
                if (value != this._updateContinuously)
                {
                    this._updateContinuously = value;
                    base.OnPropertyChangedWithValue(value, "UpdateContinuously");
                }
            }
        }

        [DataSourceProperty]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChangedWithValue(value, "Name");
                }
            }
        }

        // Token: 0x17000245 RID: 581
        // (get) Token: 0x0600079C RID: 1948 RVA: 0x0001CB82 File Offset: 0x0001AD82
        [DataSourceProperty]
        public string OptionValueAsString
        {
            get
            {
                return this.GetValueAsString();
            }
        }

        // Token: 0x0600079D RID: 1949 RVA: 0x0001CB8A File Offset: 0x0001AD8A
        public void UpdateValue()
        {

        }

        // Token: 0x0600079E RID: 1950 RVA: 0x0001CBBF File Offset: 0x0001ADBF
        public void Cancel()
        {
            this.OptionValue = this._initialValue;
            //this.UpdateValue();
        }

        // Token: 0x0600079F RID: 1951 RVA: 0x0001CBD3 File Offset: 0x0001ADD3
        public void SetValue(int value)
        {
            this.OptionValue = value;
        }

        // Token: 0x060007A0 RID: 1952 RVA: 0x0001CBDC File Offset: 0x0001ADDC
        public void ResetData()
        {
            //this.OptionValue = this.Option.GetDefaultValue();
        }

        // Token: 0x060007A1 RID: 1953 RVA: 0x0001CBEF File Offset: 0x0001ADEF
        public bool IsChanged()
        {
            return this._initialValue != this.OptionValue;
        }

        // Token: 0x060007A2 RID: 1954 RVA: 0x0001CC02 File Offset: 0x0001AE02
        public void ApplyValue()
        {
            if (this._initialValue != this.OptionValue)
            {
                this._initialValue = this.OptionValue;
            }
        }

        // Token: 0x0400038B RID: 907
        private float _initialValue;

        // Token: 0x0400038C RID: 908
        private INumericOptionData _numericOptionData;

        // Token: 0x0400038D RID: 909
        private float _discreteIncrementInterval;

        // Token: 0x0400038E RID: 910
        private float _min;

        // Token: 0x0400038F RID: 911
        private float _max;

        // Token: 0x04000390 RID: 912
        private float _optionValue;

        // Token: 0x04000391 RID: 913
        private bool _isDiscrete;

        // Token: 0x04000392 RID: 914
        private bool _updateContinuously;

        private string _name;
    }
}
