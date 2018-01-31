using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    [Serializable]
    class ATSBase 
    { 
            public int DataTime; ////时间戳
            public int Signer;//标识符 
    }

    [Serializable]
    class CommonData : ATSBase
    {
            public int LineNumber;//线路编号
            public int SourceLineNumber;//源线路号
            public int DestinationLineNumber;//目的地线路号
            public int DestinationNumber;//目的地号
            public int LineNumberOfcar;//车组所属线路号
            public int NumberOfcarGroup;//车组号,车底号
            public int NumberOfcar;//车次号
    }

    [Serializable]
    class TrainInit : CommonData
    {
            public string DataBaseName;//数据库名称
            public string ProgramName;//项目名称
            public string TractionCurveName;//牵引曲线名称
            public int PacketVersion;//信息包版本号
            public override string ToString()
            {
                return DataBaseName + "," + ProgramName + "," + TractionCurveName + "," + PacketVersion + "," + DataTime;

            }
    }

    //第二个包
    [Serializable]
    class ATStoVOBCcommand : CommonData  
    {        
            public int  TableName; //服务号，表号
            public string ZCID; //下一ZC区域ID
            public string CIID; //下一CI区域ID
            public string ATSID; //下一ATS区域ID
            public int PlanningDirection; //计划运行方向
            public int StopJumpingPlatform; //跳停站台ID
            public int ArrivePlatform; //到站站台ID
            public int NextStopPlatform; //下一停车站台ID
            public int StopTime; //站停时间
            public int NextStopCommand; //下一站跳停命令
            public int IntervalCommand; //区间运行调整命令
            public int ButtonvehiclePlatform; //扣车站台ID
            public int RoundCommand; //折返命令
            public int BackOffInstructions; //回断指示命令
            public int DoorStrategy;//门控策略
            public int RunCommand;//运行计划命令（变长）
            //预留位 未写 public 。。。。
    }

    //第三个包
    [Serializable]
    class VOBCtoATSstatus : CommonData
    {
            public int TableName; //服务号，表号
            public int drivierNum;//司机号
            public int workStatus;//ATO工作模式预留。。。。
            public int IntervalCommand; //区间运行调整命令
            public int JumpStopCommandCallback; //跳停命令反馈
            public int ButtonvehicleCommandCallback; //扣车命令反馈
            public int NextStopPlatformCallback; //下一停车站台ID命令反馈
            public int reserved;//预留位。。。。
            public int destinationID;//目的站台ID
            public int IntervalLevel;//区间运行等级或时间
            public int JumpStopStatus;//跳停状态
            public int ButtonvehicleStatus;//扣车状态
            public int NextStopPlatformID; //下一停车站台ID
            public int RunCommandCallback;//运行计划命令反馈
            public int workingMode;//车载工作模式
            public int TrainLocationStatus;//列车定位状态
            public int RunningDirection; //运行方向
            public int ActivePort;//激活端
            public int wheelsTurn;//车轮转向
            public int MaxSafeFrontSpaceID;//最大安全前端所在轨道逻辑区段ID
            public int MaxSafeFrontSpaceOffset;//最大安全前端所在轨道逻辑区段偏移量
            public int MinSafeFrontSpaceID;//最小安全前端所在轨道逻辑区段ID
            public int MinSafeFrontSpaceOffset;//最小安全前端所在轨道逻辑区段偏移量
            public int MaxSafeBehindSpaceID;//最大安全后端所在轨道逻辑区段ID
            public int MaxSafeBehindSpaceOffset;//最大安全后端所在轨道逻辑区段偏移量
            public int MinSafeBehindSpaceID;//最小安全后端所在轨道逻辑区段ID
            public int MinSafeBehindSpaceOffset;//最小安全后端所在轨道逻辑区段偏移量、
            public int TrainCurrentATPstatus;//车载ATP当前模式
            public int TrainComplete;//列车完整性
            public int TrainEmergencyLockStatus;//列车紧急制动状态
            public int TrainSpeed;//列车速度信息
            public int TrainDoorStatus;//车门状态
            public int TrainSafeStopStatus;//列车停稳状态
            public int StopEnsureStatus;//停车保证状态（万博：协会要求，上海无此状态）
            public int NoOneReturnStatus;//无人折返状态
            public int reservedMode;//预选模式。。。。
            public int TrainEmergencyLockReason;//列车紧急制动原因
            public int CurrentEmergencyLockSpeed;//当前紧急制动触发速度
            public int CurrentRecommendedSpeed;//当前推荐速度(仿真传当前ATO命令速度）(列车超速时，先触发常用制动，常用制动触发速度即当前推荐速度)
            public int AuthorizationID;//移动授权终端所在轨道逻辑区段ID
            public int AuthorizationIDOffset; //移动授权终端所在轨道逻辑区段偏移量
            public int CommonStopLocationID;//常用停车点位置所在轨道逻辑区段ID
            public int CommonStopLocationOffset; //常用停车点位置所在轨道逻辑区段偏移量
            public int SwitchInformationCount;//道岔信息个数（万博：根据道岔个数变长，结合仿真实际情况决定采用定长还是变长）
            public int Switch1ID;//道岔1 ID
            public int Switch1Status;//道岔1 状态
            public int Switch2ID;//道岔2 ID
            public int Switch2Status;//道岔2 状态
            //20个未写
            public int TheTrain;//列车编组
            public int TrainBehindSpaceID;//车尾包络所在轨道逻辑区段ID
            public int TrainBehindSpaceOffset;//车尾包络所在轨道逻辑区段偏移量
            public int TrainFrontSpaceID;//车头包络所在轨道逻辑区段ID
            public int TrainFrontSpaceOffset;//车头包络所在轨道逻辑区段偏移量
            public int TrainBehindLocationSpaceID;//车尾位置所在轨道逻辑区段ID
            public int TrainBehindLocationSpaceOffset;//车尾位置所在轨道逻辑区段偏移量
            public int TrainFronLocationtSpaceID;//车头位置所在轨道逻辑区段ID
            public int TrainFrontLocationSpaceOffset;//车头位置所在轨道逻辑区段偏移量
            public int CurrentGEBRspeed;//当前GEBR触发速度
            public int TrainStatus;//列车运行状态
            //预留位 未写 public 。。。。
    }

    //第四个包
    [Serializable]
    class ATStoZCinformation : ATSBase
    {
            public int ZCspaceID;//ZC区域ID
            public int PlatformID;//站台ID
            public int JumpStopCommand;//跳停命令
            public int ButtonvehicleCommand;//扣车命令
            public int RushTrainCommand;//催发车命令
            public int GoReturnCommand;//折返命令
            public int DestinationNumber;//目的地号
            public int StopTimeSetCommand;//停站时间设置命令
            public int StationEmergencyshutdownCommand;//站台紧急关闭命令
            public int SetTempoSpeedLimitCommands;//设置临时限速命令
            public int TempLimitStartID;//临时限速起点轨道逻辑区段ID
            public int TempLimitStartOffset;//临时限速起点轨道逻辑区段偏移量
            public int TempLimitEndID;//临时限速终点轨道逻辑区段ID
            public int TempLimitEndOffset;//临时限速终点轨道逻辑区段偏移量
            public int StartTime;//开始时间
            public int EndTime;//结束时间
            public int LimitSpeed;//限速值
            public int SetOrbitalStatusCommand;//设置轨道区段/计轴区段状态命令
            public int OrbitalStartID;//轨道区段/计轴区段起点轨道区段ID
            public int OrbitalStartOffset;//轨道区段/计轴区段起点轨道区段偏移量
            public int OrbitalEndID;//轨道区段/计轴区段终点轨道区段ID
            public int OrbitalEndOffset;//轨道区段/计轴区段终点轨道区段偏移量
            public int OrbitalStatus;//轨道区段/计轴区段状态
            public int SetSignalID;//设置信号机ID
            public int SignalModel;//信号机模式
            public int SignalStatus;//信号机模式
            public int ArtificialCommand;//人工办理进路命令（预留）
            public int EnterStartSignalID;//进路始端信号机ID
            public int EnterEndSignalID;//进路始端信号机ID
            public int MeterShaftID;//计轴器ID
            public int MeterShaftStatus;//计轴器状态
            public int SetBeaconCommand;//设置信标状态命令（预留）
            public int BeaconStatus;//设置信标状态命令（预留）
            public int SwitchID;//设置道岔ID
            public int OperateModel;//操作模式
            public int SetSwitchTargetStatus;//设置道岔目标状态（手动模式预留)
    }

    //第五个包
    [Serializable]
    class ZCtoATSinformation : ATSBase
    {
            public int ZCspaceID;//ZC区域ID
            public int JumpStopCommand;//跳停命令
            public int OrbitalStatus;//轨道区段/计轴区段起点轨道区段状态
            public int ArtificialCommand;//人工办理进路命令（预留）


            public int ButtonvehicleCommandCallback;//扣车命令反馈
            public int RushTrainCommandCallback;//催发车命令反馈
            public int GoReturnCommandCallback;//折返命令反馈
            public int StopTimeSetCommandCallback;//停站时间设置命令反馈
            public int StationEmergencyshutdownCommandCallback;//站台紧急关闭命令反馈
            public int OrbitalID;//轨道区段/计轴区段轨道区段ID
            public int SetTempoSpeedLimitCommandsCallback;//设置临时限速命令反馈
            public int SetOrbital;//设置轨道区段/计轴区段
            public int StatusCommandCallback;//状态命令反馈
            public int ArtificialSetSignalCallback;//人工设置信号机状态反馈
            public int SignalID;//信号机ID
            public int SignalModel;//信号机模式
            public int ArtificialSetMeterShaftStatusCallback;//人工设置计轴器状态反馈
            public int ArtificialSetBeaconStatusCallback;//人工设置信标状态反馈
            public int ArtificialSetSwitchStatusCallback;//人工设置道岔状态反馈
            public int SwitchID;//设置道岔ID
            public int SwitchStatus;//道岔状态
    }
}
