using UnityEngine;

[ExecuteInEditMode]
public class PlayerChanger : MonoBehaviour
{
	
	public GameObject VRPlayer, PCPlayer;
	
	public void ChangePlayerType()
	{
		if(VRPlayer.activeSelf && !PCPlayer.activeSelf)
		{
			VRPlayer.SetActive(false);
			PCPlayer.SetActive(true);
		}else if(!VRPlayer.activeSelf && PCPlayer.activeSelf)
		{
			VRPlayer.SetActive(true);
			PCPlayer.SetActive(false);
			
		}else
		{
			//either both of the players are enabled or none of them are
			// tiebreak : choose VR player
			VRPlayer.SetActive(true);
			PCPlayer.SetActive(false);
			
			
		}
	}

}
