import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemConfigurateComponent } from './system-configurate.component';

describe('SystemConfigurateComponent', () => {
  let component: SystemConfigurateComponent;
  let fixture: ComponentFixture<SystemConfigurateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SystemConfigurateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemConfigurateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
