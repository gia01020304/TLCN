import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { SystemConfigurate } from 'src/app/@models/system-configurate';
import { RxFormBuilder } from '@rxweb/reactive-form-validators';
import 'rxjs/add/operator/map'
import { NbToastrService, NbGlobalPhysicalPosition } from '@nebular/theme';
import { SystemConfigurateService } from 'src/app/@services/system-configurate.service';

@Component({
  selector: 'app-system-configurate',
  templateUrl: './system-configurate.component.html',
  styleUrls: ['./system-configurate.component.css']
})
export class SystemConfigurateComponent implements OnInit {
  constructor(private route: ActivatedRoute, private frmBuilder: RxFormBuilder, private toast: NbToastrService, private scService: SystemConfigurateService) {

  }
  frmSC: FormGroup;
  model: SystemConfigurate;
  ngOnInit() {
    this.loadSC();
  }
  test(preventDuplicates) {
    this.toast.show(
      'This is super toast message',
      `This is toast number: `,
      { preventDuplicates });
  }
  saveSC() {
    if (this.frmSC.valid) {
      let model: SystemConfigurate = Object.assign({}, this.frmSC.value);
      this.scService.saveSystemConfigurate(model).subscribe(resp => {
        this.toast.show(resp, 'Success', { status: 'success',position:NbGlobalPhysicalPosition.BOTTOM_RIGHT });
      }, errors => {
        this.toast.show(errors, 'Error', { status: 'danger',position:NbGlobalPhysicalPosition.BOTTOM_RIGHT});
      })
    }
  }
  loadSC() {
    this.route.data.subscribe((data) => {
      this.frmSC = this.frmBuilder.formGroup(SystemConfigurate, data["model"]);
    })
  }

}
