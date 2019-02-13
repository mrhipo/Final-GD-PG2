using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievements",menuName = "Achievement/CreateAll",order = 1)]
public class AchievementsFactory : ScriptableObject
{
	public List<Achievement> allAchievements;
	public AchievementPopUp achievementPopUp;

	private Queue<Achievement> achievementsQueue = new Queue<Achievement>();
	private AchievementPopUp crrPopUp;
	private Coroutine processingQueueCoroutine;
	
	public void Init()
	{
		GlobalEvent.Instance.AddEventHandler<AchievementCompleteEvent>(OnAchievementCompleteEvent);
	}

	public void Remove()
	{
		GlobalEvent.Instance.RemoveEventHandler<AchievementCompleteEvent>(OnAchievementCompleteEvent);
	}
	
	private void OnAchievementCompleteEvent(AchievementCompleteEvent gameEvent)
	{
		CompleteAchievement(gameEvent.type);
	}

	private void CompleteAchievement(AchievementType type)
	{
		foreach (var item in allAchievements)
		{
			if (!item.IsType(type)) continue;
            item.completed = true;
			achievementsQueue.Enqueue(item);
			if(processingQueueCoroutine == null)
				processingQueueCoroutine = CoroutineManager.Instance.RunCoroutine(ProcessQueue());
		}
	}

    public void MakeComplete(AchievementType type)
    {
        foreach (var item in allAchievements)
        {
            if (!item.IsType(type)) continue;
            item.completed = true;
        }
    }
    public bool CheckComplete(AchievementType type)
    {
        foreach (var item in allAchievements)
            if (item.type == type)
                return item.completed;
        return false;
    }

    private IEnumerator ProcessQueue()
	{
		do
		{
			crrPopUp = crrPopUp ? crrPopUp : GameObject.Instantiate(achievementPopUp);
			crrPopUp.gameObject.SetActive(true);
			crrPopUp.SetAchievement(achievementsQueue.Dequeue());
			yield return new WaitForSeconds(2);
			crrPopUp.gameObject.SetActive(false);
		} while (achievementsQueue.Count > 0);
		
	}

	
}

