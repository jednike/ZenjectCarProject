using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UniRx;
using UnityEngine;
using Zenject;

namespace CarsTest
{
    public class NetworkConnector : IInitializable, IDisposable, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
    {
        private readonly GameInfo _gameInfo;
        private readonly GameStateManager _stateManager;
        
        private PlayerType _playerType;
        
        public NetworkConnector(GameInfo gameInfo, GameStateManager stateManager)
        {
            _gameInfo = gameInfo;
            _stateManager = stateManager;
        }

        public virtual void Initialize()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void ConnectNow(PlayerType playerType)
        {
            _playerType = playerType;
            
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = Application.version;
        }
        public void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }

        #region Connecting
        
        public void OnConnected()
        {
            
        }

        public void OnConnectedToMaster()
        {
            CreateOrJoinRoom();
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
            
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
            
        }

        #endregion

        #region Lobby

        public void OnJoinedLobby()
        {
            CreateOrJoinRoom();
        }

        public void OnLeftLobby()
        {
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
        }

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
        }

        #endregion

        #region Mathcmaking

        private void CreateOrJoinRoom()
        {
            _gameInfo.playerType = _playerType;
            if (_playerType == PlayerType.Runner)
                PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = 5, CustomRoomProperties = new Hashtable{{"Level", _gameInfo.selectedLevel}}});
            else
                PhotonNetwork.JoinRandomRoom();
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
            Observable.Timer(TimeSpan.FromSeconds(5)).Subscribe(WaitForNewSearch);
        }

        private void WaitForNewSearch(long obj)
        {
            CreateOrJoinRoom();
        }

        public void OnLeftRoom()
        {
        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
        }

        public void OnCreatedRoom()
        {
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
        }

        public void OnJoinedRoom()
        {
            _gameInfo.selectedLevel = (int) PhotonNetwork.CurrentRoom.CustomProperties["Level"];
            PhotonNetwork.IsMessageQueueRunning = false;
            _stateManager.ChangeState(GameState.Loading);
        }

        #endregion

        public virtual void Dispose()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
    }
}
