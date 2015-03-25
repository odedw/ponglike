using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class RandomBoardPlacer : BoardPlacer {
    

    public override int PlaceBoard(BoardManager boardManager, int gold)
    {
        var spent = 0;
        var hashSet = new HashSet<string>();
        while (gold > 0)
        {
            var itemObject = GameManager.Instance.Store.ItemObjects[Random.Range(0, GameManager.Instance.Store.ItemObjects.Length)];
            var instantiatedItem = Instantiate(itemObject);
            var price = instantiatedItem.GetComponent<Item>().Price;
            spent += price;
            gold -= price;
            Vector3 position;
            do
            {
                position = new Vector3(
                    Random.Range(0, GameState.Instance.Columns - 1),
                    Random.Range(1, GameState.Instance.Rows - 2),
                    0);    
            } while (hashSet.Contains(position.ToString()));
            hashSet.Add(position.ToString());
            GameManager.Instance.BoardManager.PlaceItem(position, instantiatedItem);
        }
        return spent;
    }
}
