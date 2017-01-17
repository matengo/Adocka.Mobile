using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Adocka.Mobile.Helpers
{
    public class MvvmCommand : Command
    {
        public MvvmCommand(Action<Object> action, Expression<Func<Object, bool>> propExpression) : base(action, propExpression.Compile())
        {
            var member = propExpression.Body as MemberExpression;
            var expression = member.Expression as ConstantExpression;

            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' should be a property.",
                    propExpression.ToString()));
            if (expression == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' should be a constant expression",
                    propExpression.ToString()));
            var viewModel = (INotifyPropertyChanged)expression.Value;
            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propExpression.ToString()));
            var propertyName = propInfo.Name;
            viewModel.PropertyChanged += (sender, e) => {
                if (e.PropertyName == propertyName)
                {
                    this.ChangeCanExecute();
                };
            };
        }
    }
}
