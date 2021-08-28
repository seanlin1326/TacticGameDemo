using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public PieceDeck player1Deck;
    public PieceDeck player2Deck;

    public List<Transform> spawnPointList;
  [SerializeField]  private List<PieceData> player1Pieces=new List<PieceData>();
   [SerializeField] private List<PieceData> player2Pieces=new List<PieceData>();
    public int emptyCellNum;
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    private void Start()
    {

        player1Pieces.Clear();
        foreach (var i in player1Deck.PieceList)
        {
            player1Pieces.Add(i);
        }
        foreach (var i in player2Deck.PieceList)
        {
            player2Pieces.Add(i);
        }
        ShuffleTest(player1Pieces);
        ShuffleTest(player2Pieces);
        int _player1Count=player1Pieces.Count;
        int _player2Count=player2Pieces.Count;
        emptyCellNum = spawnPointList.Count - _player1Count - _player2Count;
        for (int i=0;i<spawnPointList.Count; i++)
        {
            _player1Count = player1Pieces.Count;
            _player2Count = player2Pieces.Count;
          
            int _randomTemp = Random.Range(0,_player1Count+_player2Count+emptyCellNum);
            if (_randomTemp <_player1Count)
            {
                GameObject _Spawn = Instantiate(player1Prefab, spawnPointList[i].position, Quaternion.identity);
                _Spawn.GetComponent<Piece>().pieceData = player1Pieces[0];
                player1Pieces.RemoveAt(0);
               
            }
            else if (_randomTemp >=_player1Count &&_randomTemp < _player1Count + _player2Count)
            {
                GameObject _Spawn = Instantiate(player2Prefab, spawnPointList[i].position, Quaternion.identity);
                _Spawn.GetComponent<Piece>().pieceData = player2Pieces[0];
                player2Pieces.RemoveAt(0);
              
            }
            else
            {
                emptyCellNum--;
            }
        }

    }

    public void Shuffle()
    {
        int _player1Count;
        int _player2Count;
        foreach (var i in player1Deck.PieceList)
        {
            player1Pieces.Add(i);
        }
        foreach (var i in player2Deck.PieceList)
        {
            player2Pieces.Add(i);
        }
        _player1Count = player1Pieces.Count;
        _player2Count = player2Pieces.Count;
        emptyCellNum = spawnPointList.Count - _player1Count - _player2Count;

        for (int i = 0; i < spawnPointList.Count; i++)
        {
            if (player1Pieces.Count != 0)
            {
                GameObject _player1Spawn = Instantiate(player1Prefab, spawnPointList[i].position, Quaternion.identity);
                _player1Spawn.GetComponent<Piece>().pieceData = player1Pieces[7];
            }
        }
    }
    public void ShuffleTest(List<PieceData> _Deck)
    {
        PieceData _temp1;
        for (int i = 0; i < _Deck.Count; i++)
        {
            int _currentIndex = Random.Range(0, _Deck.Count-i);
            _temp1 = _Deck[_currentIndex];
            _Deck[_currentIndex] = _Deck[_Deck.Count - i-1];
            _Deck[_Deck.Count - i - 1] = _temp1;


        }
    }

   
}
