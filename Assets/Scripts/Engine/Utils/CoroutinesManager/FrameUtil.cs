using System;
using System.Collections;
using UnityEngine;


public static class FrameUtil
{
	public static void AtEndOfFrame(Action action)
	{
		CoroutineManager.Instance.RunCoroutine(RunAtEndOfFrame(action));
	}

	public static void OnNextFrame(Action action)
	{
		CoroutineManager.Instance.RunCoroutine(RunOnNextFrame(action));
	}

	public static void AfterFrames(int frames, Action action)
	{
		CoroutineManager.Instance.RunCoroutine(RunAfterFrames(frames, action));
	}

	public static void AfterDelay(float delayInSeconds, Action action)
	{
		CoroutineManager.Instance.RunCoroutine(RunAfterDelay(delayInSeconds, action));
	}

	public static void RepeatAction(float delayInSeconds, int count,Action action, Action lastAction)
	{
		CoroutineManager.Instance.RunCoroutine(RepeatActionCoroutine(delayInSeconds,count, action, lastAction));
	}

	private static IEnumerator RepeatActionCoroutine(float delayInSeconds, int count, Action action, Action lastAction)
	{
		var waitSeconds = new WaitForSeconds(delayInSeconds);
		for (int i = 0; i < count; i++)
		{
			action();
			yield return waitSeconds;
		}
		lastAction();
	}


	private static IEnumerator RunAtEndOfFrame(Action action)
	{
		yield return new WaitForEndOfFrame();

		action();
	}

	private static IEnumerator RunOnNextFrame(Action action)
	{
		yield return null;

		action();
	}

	private static IEnumerator RunAfterFrames(int frames, Action action)
	{
		for (var x = 0; x < frames; x++)
		{
			yield return null;
		}

		action();
	}

	private static IEnumerator RunAfterDelay(float delayInSeconds, Action action)
	{
		yield return new WaitForSeconds(delayInSeconds);

		action();
	}
}