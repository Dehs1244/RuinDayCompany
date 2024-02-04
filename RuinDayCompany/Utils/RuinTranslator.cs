using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RuinDayCompany.Utils
{
    public class RuinTranslator
    {
        public void LocalTransmitMessage(string signalMessage)
        {            
            HUDManager.Instance.StartCoroutine(this._DisplaySignalTranslator(signalMessage));
        }

        private IEnumerator _DisplaySignalTranslator(string message)
        {
            System.Random signalMessageRandom = new System.Random(StartOfRound.Instance.randomMapSeed);
            HUDManager.Instance.signalTranslatorAnimator.SetBool("transmitting", true);

            HUDManager.Instance.signalTranslatorText.text = "";
            yield return new WaitForSeconds(1.21f);
            int i = 0;
            while (i < message.Length)
            {
                HUDManager.Instance.signalTranslatorText.text = HUDManager.Instance.signalTranslatorText.text + message[i].ToString();
                float num = Mathf.Min((float)signalMessageRandom.Next(-1, 4) * 0.5f, 0f);
                yield return new WaitForSeconds(0.7f + num);
                int num2 = i;
                i = num2 + 1;
            }
            yield return new WaitForSeconds(0.5f);
            HUDManager.Instance.signalTranslatorAnimator.SetBool("transmitting", false);

            yield break;
        }
    }
}
