Imports System.Runtime.InteropServices
'http://web.archive.org/web/20110910100053/http://www.indigo79.net/archives/27#comment-255
Public Class AdvancedTimer

    ''' <summary>
    ''' Timer type.
    ''' 0 = System.Threading ( default )
    ''' 1 = Windows.Forms
    ''' 2 = Microtimer ( custom code ).
    ''' 3 = Multimedia timer ( windows mm dll ).
    ''' </summary>
    Private _timerType As Integer

    Private _timer0 As System.Threading.Timer

    Private _timer1 As System.Windows.Forms.Timer

    Private _interval As Integer

    Private _elapsedTimer0Handler As ElapsedTimer0Delegate

    Private _elapsedTimer1Handler As ElapsedTimer1Delegate

    Private _elapsedTimer3Handler As ElapsedTimer3Delegate

    Private _elapsedTimerHandler As ElapsedTimerDelegate

    Private _enabled As Boolean

    Public Sub New(timerType As Integer, intervalMS As Integer, callback As ElapsedTimerDelegate)
        _timerType = timerType

        _interval = intervalMS

        _elapsedTimerHandler = callback

    End Sub

    Public Delegate Sub ElapsedTimerDelegate()

    Public Delegate Sub ElapsedTimer0Delegate(sender As Object)

    Public Delegate Sub ElapsedTimer1Delegate(sender As Object, e As EventArgs)

    Public Delegate Sub ElapsedTimer3Delegate(tick As Integer, span As TimeSpan)

    'private delegate void TestEventHandler(int tick, TimeSpan span);


    Private Sub Timer3Handler(id As Integer, msg As Integer, user As IntPtr, dw1 As Integer, dw2 As Integer)

        _elapsedTimerHandler()

    End Sub

    Public Sub Start()

        timeBeginPeriod(1)
        mHandler = New TimerEventHandler(AddressOf Timer3Handler)
        mTimerId = timeSetEvent(_interval, 0, mHandler, IntPtr.Zero, EVENT_TYPE)
        mTestStart = DateTime.Now
        mTestTick = 0

    End Sub

    Public Sub [Stop]()

        Dim err As Integer = timeKillEvent(mTimerId)
        timeEndPeriod(1)
        mTimerId = 0

    End Sub

    Private mTimerId As Integer
    Private mHandler As TimerEventHandler
    Private mTestTick As Integer
    Private mTestStart As DateTime

    ' P/Invoke declarations
    Private Delegate Sub TimerEventHandler(id As Integer, msg As Integer, user As IntPtr, dw1 As Integer, dw2 As Integer)

    Private Const TIME_PERIODIC As Integer = 1
    Private Const EVENT_TYPE As Integer = TIME_PERIODIC

    ' + 0x100;  // TIME_KILL_SYNCHRONOUS causes a hang ?!
    <DllImport("winmm.dll")> _
    Private Shared Function timeSetEvent(delay As Integer, resolution As Integer, handler As TimerEventHandler, user As IntPtr, eventType As Integer) As Integer
    End Function

    <DllImport("winmm.dll")> _
    Private Shared Function timeKillEvent(id As Integer) As Integer
    End Function

    <DllImport("winmm.dll")> _
    Private Shared Function timeBeginPeriod(msec As Integer) As Integer
    End Function

    <DllImport("winmm.dll")> _
    Private Shared Function timeEndPeriod(msec As Integer) As Integer
    End Function

End Class
