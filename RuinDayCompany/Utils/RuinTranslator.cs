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
            SignalTranslator signalTranslator = UnityEngine.Object.FindObjectOfType<SignalTranslator>();
            signalTranslator.timeLastUsingSignalTranslator = Time.realtimeSinceStartup;
            if (signalTranslator.signalTranslatorCoroutine != null)
            {
                HUDManager.Instance.StopCoroutine(signalTranslator.signalTranslatorCoroutine);
            }

            signalTranslator.signalTranslatorCoroutine = HUDManager.Instance.StartCoroutine(this._DisplaySignalTranslator(signalMessage, signalTranslator.timesSendingMessage, signalTranslator));
        }

        private IEnumerator _DisplaySignalTranslator(string message, int seed, SignalTranslator translator)
        {
            System.Random signalMessageRandom = new System.Random(seed + StartOfRound.Instance.randomMapSeed);
            HUDManager.Instance.signalTranslatorAnimator.SetBool("transmitting", true);
            translator.localAudio.Play();
            HUDManager.Instance.UIAudio.PlayOneShot(translator.startTransmissionSFX, 1f);

            HUDManager.Instance.signalTranslatorText.text = "";
            yield return new WaitForSeconds(1.21f);
            int i = 0;
            while (i < message.Length && !(translator == null) && translator.gameObject.activeSelf)
            {
                HUDManager.Instance.UIAudio.PlayOneShot(translator.typeTextClips[UnityEngine.Random.Range(0, translator.typeTextClips.Length)]);
                HUDManager.Instance.signalTranslatorText.text = HUDManager.Instance.signalTranslatorText.text + message[i].ToString();
                float num = Mathf.Min((float)signalMessageRandom.Next(-1, 4) * 0.5f, 0f);
                yield return new WaitForSeconds(0.7f + num);
                int num2 = i;
                i = num2 + 1;
            }
            if (translator != null)
            {
                HUDManager.Instance.UIAudio.PlayOneShot(translator.finishTypingSFX);
                translator.localAudio.Stop();
            }
            yield return new WaitForSeconds(0.5f);
            HUDManager.Instance.signalTranslatorAnimator.SetBool("transmitting", false);

            yield break;
        }
    }
}
