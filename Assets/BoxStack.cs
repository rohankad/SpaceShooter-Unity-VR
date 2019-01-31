using UnityEngine;
using System.Collections;

public class BoxStack : MonoBehaviour {
	
	//public int[] InputList = new int[] {10,20,3,5,10,5,8,2,46,22};
	ArrayList InputList = new ArrayList();  

	public ArrayList OutList1 = new ArrayList();
	public ArrayList DuplicateList = new ArrayList();
	public int[] results;
	// Use this for initialization
	void Start () {
		results = new int[] {10,20,3,5,10,5,8,2,46,22};

	//	InputList.Add (results[3]);
		
		for (int i= 0; i<results.Length; i++) {
			
			InputList.Add(results[i]);
			
		}

		for (int i= 0; i<InputList.Count; i++) {
			
			//Debug.Log ("I L    "+InputList[i]);
			
		}

		InputList.Sort ();


		for (int i= 0; i<InputList.Count; i++) {
			
			//Debug.Log ("I L S   "+InputList[i]);
			
		}


		for (int i= 0; i<OutList1.Count; i++) {
			
			Debug.Log ("O L   : "+OutList1[i]);
			
		}

		calculations ();
	}
	
	void calculations(){
		OutList1.Add(InputList[0]);
		//OutList1.Add(InputList[8]);

		for (int i = 0; i < InputList.Count; i++) {

			if (OutList1.Contains (InputList [i])) {
				
				DuplicateList.Add (InputList [i]);
				print ("DuplicateList : "+InputList[i]);
			} else {
				OutList1.Add (InputList [i]);
				print ("Out : "+InputList[i]);
			}
		}
		/*

		OutList1.Add(InputList[InputList.Count-1]);

		for (int i= InputList.Count-1; i<InputList.Count; i--) {

			if(OutList1[i]!=InputList[i+1]){

				OutList1.Add(InputList[i]);
				print (" List : "+InputList[i]);
			}

		} */
	
	}


void StortingAlgo(){


		for (int i = InputList.Count - 1; i >= 0; i--) {


			if (InputList [i] == InputList [i - 1]) {
				Debug.Log ("%%%%%   :" + i);
				Debug.Log ("HERE I AM " + InputList [i]);
				//InputList.RemoveAt(i+1);
				//InputList.Add(InputList[i+1]);
			}
		}


	}

}
