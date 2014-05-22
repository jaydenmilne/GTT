<Serializable()>
Public Class HealthManager
    <Serializable()>
    Public Structure CurrentHealthHolder

        Public ShieldPoints As Double
        Public HullPoints As Double
        Public Damage As Double ' damage is how much damage it does, not how much damage it has
        Public Dead As Boolean
        Public RechargeRate As Double

    End Structure


    Public Health As CurrentHealthHolder

    Public MaxHealth As CurrentHealthHolder

    Public Enum ShieldStatus
        Full
        Charging
        Down
    End Enum

    Public LastHit As Long

    Sub New(ByVal PassedHullPoints As Integer, ByVal PassedShieldPoints As Integer, ByVal PassedRechargeRate As Double, ByVal PassedDamage As Double)

        Health.HullPoints = PassedHullPoints

        Health.ShieldPoints = PassedHullPoints

        Health.RechargeRate = PassedRechargeRate

        Health.Damage = PassedDamage

        Health.Dead = False



        MaxHealth.HullPoints = PassedHullPoints

        MaxHealth.ShieldPoints = PassedHullPoints

        MaxHealth.RechargeRate = PassedRechargeRate

        MaxHealth.Damage = PassedDamage

        MaxHealth.Dead = False

    End Sub

    Sub New(ByVal PassedWad As CurrentHealthHolder)
        Health = PassedWad
        MaxHealth = PassedWad
    End Sub



    Public Sub Hit(ByVal OtherID As Integer, ByVal OtherType As Entities) ' may want to have different things be better against other things eventually


        Dim EntityManager = MainForm.Game.EntityManager ' too lazy to keep typing this

        Dim OtherHealthWad = EntityManager.WadOEntities(OtherID).GetHealthWad ' too lazy to keep typing this

        If Not OtherHealthWad.Dead And Not Health.Dead Then ' ensure that both are alive. No zombies in this game

            Dim DamageToDo = OtherHealthWad.Damage

            While DamageToDo > 0 ' ensure that all damage points are dished out to shield and etc

                If Health.ShieldPoints > 0 Then

                    Health.ShieldPoints -= 1
                    DamageToDo -= 1

                Else

                    Health.HullPoints -= 1
                    DamageToDo -= 1

                End If

            End While

            If Health.HullPoints <= 0 Then

                Health.Dead = True

            End If

            LastHit = MainForm.Game.GameWatch.ElapsedMilliseconds

        End If



    End Sub

    Public Sub Update(ByVal d As Double)


        If (MainForm.Game.GameWatch.ElapsedMilliseconds - LastHit) > Health.RechargeRate Then ' check to see if 10s has elapsed

            If Health.ShieldPoints < MaxHealth.ShieldPoints Then ' make sure we aren't adding more to shield than it can hold

                Health.ShieldPoints += 0.09 * d

                If Health.ShieldPoints > MaxHealth.ShieldPoints Then ' check if we overshot

                    Health.ShieldPoints = MaxHealth.ShieldPoints

                End If
            End If



        End If





    End Sub






End Class
