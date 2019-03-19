using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreComparator : IComparer  {

      // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
      int IComparer.Compare( System.Object x, System.Object y  )  {
		  if(x == null || y == null){
			  return 0;
		  }
          return( (new CaseInsensitiveComparer()).Compare( ((GameObject)y).GetComponent<PlayerFire>().score, 
		  ((GameObject)x).GetComponent<PlayerFire>().score ) );
      }

   }