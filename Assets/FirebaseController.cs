using Firebase;
using Firebase.Database;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseController : MonoBehaviour
{
    public static FirebaseController Instance  { get; private set; }
    DatabaseReference database;
    FirebaseApp app;
    private string dbLink = "https://secret-hitler-7e88f-default-rtdb.europe-west1.firebasedatabase.app/";
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        Debug.Log("Test?");
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            
            var status = task.Result;
            if(status != DependencyStatus.Available)
            {
                Debug.Log($"Firebase dependency error {status}");
                return;
            }
            app = FirebaseApp.DefaultInstance;
            InitFirebase().ContinueWith(t => {
                Debug.Log("IsCompleted"); Debug.Log(t.IsCompleted);
                Debug.Log(t.Result.Value.ToString());
               
            });
            Debug.Log("Firebase Initialized Successfully");
        });
    }
    async Task<DataSnapshot> InitFirebase()
    {
        FirebaseDatabase.DefaultInstance.GoOnline();
        FirebaseDatabase.GetInstance(app).RootReference.ValueChanged += HandleValueChanged;
        var result = await FirebaseDatabase.DefaultInstance.RootReference.GetValueAsync();
        return result;
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs e)
    {
        if(e.DatabaseError != null)
        {
            Debug.Log("pizda");
            return;
        }
        Debug.Log(e.Snapshot.Value.ToString());
    }
}
