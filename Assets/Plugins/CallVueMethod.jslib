mergeInto(LibraryManager.library, {
  JoinGame: function (nickname, guid) {
    const joinEvent = new CustomEvent("JoinGame", {
      detail: {
        nickname : Pointer_stringify(nickname),
        guid : Pointer_stringify(guid),
      },
    });
    document.dispatchEvent(joinEvent);
  },  

  Mute: function (guid) {
    const muteEvent = new CustomEvent("Mute",{
      detail: {
        guid : Pointer_stringify(guid),
      },
    });
    document.dispatchEvent(muteEvent);
  },  

  Unmute: function (guid) {
    const guidToString = Pointer_stringify(guid);
    const unmuteEvent = new CustomEvent("UnMute",{
      detail: {
        guid : Pointer_stringify(guid),
      },
    });
    document.dispatchEvent(unmuteEvent);
  },  

  ShowChat: function() {
    const showchatEvent = new CustomEvent("ShowChat");
    document.dispatchEvent(showchatEvent);
  },
  HideChat: function() {
    const hidechatEvent = new CustomEvent("HideChat");
    document.dispatchEvent(hidechatEvent);
  },
  CheckPlayer: function() {
    const checkplayerEvent = new CustomEvent("CheckPlayer");
    document.dispatchEvent(checkplayerEvent);
  },
});