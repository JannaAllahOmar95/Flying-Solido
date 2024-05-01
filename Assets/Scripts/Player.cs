using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInput _inputActions;
    private Score score;

    private void Awake()
    {
        _inputActions = new PlayerInput();
        _inputActions.Enable();

        _inputActions.Android.ScreenTouch.performed += _ => TouchScreen();
        score = FindObjectOfType<Score>();
    }

    private void TouchScreen()
    {
        Vector2 _touchValue = _inputActions.Android.ScreenTouch.ReadValue<Vector2>();
        Debug.Log("Helooooo");

        Ray ray = Camera.main.ScreenPointToRay(_touchValue);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            enemy.currentHealth -= 10;

            enemy.UpdateEnemyText();

            if (enemy.currentHealth <= 0)
            {
                enemy.DestroyEnemy();
                score.IncrementScore();
            }
        }
    }
}