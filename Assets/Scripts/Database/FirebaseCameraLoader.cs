using UnityEngine;
using Firebase.Database;
using XRWorld.Database;

public class FirebaseCameraLoader : MonoBehaviour
{
    private DatabaseReference _reference;
    
    public void LevelLoadedStart()
    { 
        _reference = FirebaseDatabase.DefaultInstance.GetReference(DatabaseConstants.PLAYERS);
        Enable();
    }
    
    private void Enable()
    {
        _reference.ChildChanged += HandleChildChanged;
    }

    private void OnDisable()
    {
        _reference.ChildChanged -= HandleChildChanged;
    }

    private void HandleChildChanged(object sender, ChildChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        
        print("CAMCHANGEDDDD");
    }
}
