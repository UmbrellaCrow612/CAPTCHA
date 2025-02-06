using CAPTCHA.Core.Models;
using CAPTCHA.Core.Options;
using System.Linq;

namespace CAPTCHA.Core.Services
{
    public class KeyPadCAPTCHAService
    {
        public readonly KeyPadCAPTCHAOptions defaultOptions = new();

        public KeyPadCAPTCHAResult GenerateQuestion()
        {
            var result = new KeyPadCAPTCHAResult();
            var sep = result.CAPTCHA.GetSeparator();

            var charactersTouse = defaultOptions.CharacterSet.OrderBy(x => Guid.NewGuid().ToString()).Take((int)defaultOptions.NumberOfCharactersToUse).ToList();

            var children = new List<KeyPadChild>();

            foreach (var character in charactersTouse)
            {
                children.Add(new KeyPadChild { VisualText = character.ToString() });
            }
            result.CAPTCHA.Children = children;

            var answerChildren = children.Take((int)defaultOptions.AnswerSize).ToList();
            var answerChildrenIds = answerChildren.Select(x => x.Id);
            var concatedAnswer = string.Join(sep, answerChildrenIds);
            result.CAPTCHA.AnswerInPlainText = concatedAnswer;

            var visualAnswerInOrder = string.Join("", answerChildren.Select(x => x.VisualText));
            result.VisualAnswer = visualAnswerInOrder;

            result.Succeeded = true;

            return result;
        }
    }

    public class KeyPadCAPTCHAResult
    {
        public bool Succeeded { get; set; } = false;

        public KeyPadCAPTCHA CAPTCHA { get; set; } = new() { AnswerInPlainText = "DEFAULT" };
        public string VisualAnswer { get; set; } = "DEFAULT";
    }
}
