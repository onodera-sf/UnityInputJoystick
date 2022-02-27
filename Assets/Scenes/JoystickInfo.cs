using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class JoystickInfo : MonoBehaviour
{
  /// <summary>����\��������e�L�X�g�I�u�W�F�N�g�B</summary>
  [SerializeField] private Text TextObject;

  StringBuilder Builder = new StringBuilder();

  // �X�V�̓t���[�����Ƃ�1��Ăяo����܂�
  void Update()
  {
    if (TextObject == null)
    {
      Debug.Log($"{nameof(TextObject)} �� null �ł��B");
      return;
    }

    // �P�ڂ̃W���C�X�e�B�b�N�̏����擾
    var joystick = Joystick.current;
    if (joystick == null)
    {
      Debug.Log("�W���C�X�e�B�b�N������܂���B");
      TextObject.text = "";
      return;
    }

    Builder.Clear();

    // �W���C�X�e�B�b�N�̏��擾
    Builder.AppendLine($"deviceId:{joystick.deviceId}");
    Builder.AppendLine($"name:{joystick.name}");
    Builder.AppendLine($"displayName:{joystick.displayName}");

    // �W���C�X�e�B�b�N�ɂ̓{�^���̑啔������`����Ă��Ȃ��̂�
    // ��{�I�ɂ� allControls �ŗ񋓂��ē��͂̎�ނƒl���m�F���Ă���
    foreach (var key in joystick.allControls)
    {
      if (key is StickControl stick)
      {
        if (stick.up.isPressed) Builder.AppendLine($"{key.name} Up");
        if (stick.down.isPressed) Builder.AppendLine($"{key.name} Down");
        if (stick.left.isPressed) Builder.AppendLine($"{key.name} Left");
        if (stick.right.isPressed) Builder.AppendLine($"{key.name} Right");

        var value = stick.ReadValue();
        if (value.magnitude > 0f)
        {
          Builder.AppendLine($"{key.name}:{value.normalized * value.magnitude}");
        }
      }
      else if (key is Vector2Control vec2)
      {
        var value = vec2.ReadValue();
        if (value.magnitude > 0f)
        {
          Builder.AppendLine($"{key.name}:{value.normalized * value.magnitude}");
        }
        if (vec2.x.ReadValue() != 0f)
        {
          Builder.AppendLine($"{key.name}.x:{vec2.x.ReadValue()}");
        }
        if (vec2.y.ReadValue() != 0f)
        {
          Builder.AppendLine($"{key.name}.x:{vec2.y.ReadValue()}");
        }
      }
      else if (key is ButtonControl button)
      {
        if (button.isPressed) Builder.AppendLine($"{key.name} isPress");
        var value = button.ReadValue();
        if (value != 0f)
        {
          Builder.AppendLine($"{key.name}:{value}");
        }
      }
      else if (key is AxisControl axis)
      {
        if (axis.IsPressed()) Builder.AppendLine($"{key.name} isPress");

        var value = axis.ReadValue();
        if (value != 0f)
        {
          Builder.AppendLine($"{key.name}:{value}");
        }
      }
      else
      {
        Builder.AppendLine($"Type={key.GetType()}");
      }
    }

    // �����Ă���{�^���ꗗ���e�L�X�g�ŕ\��
    TextObject.text = Builder.ToString();
  }
}
