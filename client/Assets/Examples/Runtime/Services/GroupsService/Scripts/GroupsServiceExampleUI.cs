using System.Text;
using Beamable.Examples.Shared;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Beamable.Examples.Services.GroupsService
{
   /// <summary>
   /// The UI for the <see cref="GroupsServiceExample"/>.
   /// </summary>
   public class GroupsServiceExampleUI : ExampleCanvasUI
   {
      //  Fields  ---------------------------------------
      [SerializeField] private GroupsServiceExample _groupsServiceExample = null;

      // Menu Panel
      private TMP_Text MenuTitleText { get { return TitleText01; }}
      private Button SendMessageButton { get { return Button01;}}
      private Button CreateGroupButton { get { return Button02;}}
      private Button LeaveGroupButton { get { return Button03;}}
      
      // Groups Panel
      private TMP_Text GroupsTitleText { get { return TitleText02; }}
      private TMP_Text GroupsBodyText { get { return BodyText02; }}
      
      // Messages Panel 
      private TMP_Text MessagesTitleText { get { return TitleText03; }}
      private TMP_Text MessagesBodyText { get { return BodyText03; }}
      
      //  Unity Methods  --------------------------------
      protected void Start()
      {
         _groupsServiceExample.OnRefreshed.AddListener(GroupsServiceExample_OnRefreshed);
         SendMessageButton.onClick.AddListener(SendMessageButton_OnClicked);
         CreateGroupButton.onClick.AddListener(CreateGroupButton_OnClicked);
         LeaveGroupButton.onClick.AddListener(LeaveGroup_OnClicked);
         
         // Populate default UI
         _groupsServiceExample.Refresh();
      }

      //  Methods  --------------------------------------
      
      //  Event Handlers  -------------------------------
      private async void SendMessageButton_OnClicked()
      {
         await _groupsServiceExample.SendGroupMessage();
      }

      private async void CreateGroupButton_OnClicked()
      {
         await _groupsServiceExample.CreateGroup();
      }

      private async void LeaveGroup_OnClicked()
      {
         await _groupsServiceExample.LeaveGroup();
      }
      
      private void GroupsServiceExample_OnRefreshed(GroupsServiceExampleData 
         groupsServiceExampleData)
      {
         // Show UI: Groups
         StringBuilder stringBuilder01 = new StringBuilder();
         stringBuilder01.Append("GROUPS").AppendLine();
         foreach (string groupName in groupsServiceExampleData.GroupNames)
         {
            stringBuilder01.Append($" • {groupName}").AppendLine();
         }
         // Show UI: Rooms
         stringBuilder01.AppendLine();
         stringBuilder01.Append("ROOMS").AppendLine();
         foreach (string setScoreLog in groupsServiceExampleData.RoomNames)
         {
            stringBuilder01.Append($" • {setScoreLog}").AppendLine();
         }
         
         // Show UI: Users
         stringBuilder01.AppendLine();
         stringBuilder01.Append("USERS").AppendLine();
         foreach (string roomUsername in groupsServiceExampleData.RoomUsernames)
         {
            stringBuilder01.Append($" • {roomUsername}").AppendLine();
         }
         GroupsBodyText.text = stringBuilder01.ToString();
         
         // Show UI: Messages
         StringBuilder stringBuilder02 = new StringBuilder();
         foreach (string roomMessage in groupsServiceExampleData.RoomMessages)
         {
            stringBuilder02.Append($" • {roomMessage}").AppendLine();
         }
         MessagesBodyText.text = stringBuilder02.ToString();
         
         // Show UI: Other 
         MenuTitleText.text = "GroupsService Example";
         GroupsTitleText.text = "Groups";
         MessagesTitleText.text = "Messages";
         
         CreateGroupButton.GetComponentInChildren<TMP_Text>().text = 
            $"Create Group\n({groupsServiceExampleData.GroupToCreateName})";

         LeaveGroupButton.GetComponentInChildren<TMP_Text>().text = 
            $"Leave Group\n({groupsServiceExampleData.GroupToLeaveName})";

         SendMessageButton.GetComponentInChildren<TMP_Text>().text =
            $"Send Message\n({groupsServiceExampleData.MessageToSend})";

         bool isInGroup = groupsServiceExampleData.IsInGroup;
         CreateGroupButton.interactable = !isInGroup;
         LeaveGroupButton.interactable = isInGroup;

      }
   }
}

